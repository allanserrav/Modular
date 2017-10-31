using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoBuilder.Shared
{
    public class FileWatcherEventArgs : EventArgs
    {
        public FileInfo FileChange { get; set; }

        public bool IsOnAddList { get; set; }

        public bool IsNewFile { get; set; }

        public bool IsChange { get; set; }

        public string RootPath { get; set; }

        public string FileName { get; set; }
    }

    public class FileWatcher
    {
        private readonly PhysicalFileProvider fileProvider;
        private readonly List<FileInfo> filesWatchList;
        private readonly string root;
        private readonly TaskCompletionSource<string> source;

        public event EventHandler<FileWatcherEventArgs> OnChange;
        public event EventHandler<FileWatcherEventArgs> OnAdd;
        public event EventHandler<FileWatcherEventArgs> OnNotFound;

        public FileWatcher(string root, TaskCompletionSource<string> source)
        {
            fileProvider = new PhysicalFileProvider(root);
            this.root = root;
            this.source = source;
            filesWatchList = new List<FileInfo>();
        }

        public void AddWatch(string fileName)
        {
            string fullpath = Directory.GetFiles(root, fileName, SearchOption.AllDirectories).FirstOrDefault();
            if (!String.IsNullOrEmpty(fullpath)) {
                FileInfo info = new FileInfo(fullpath);
                OnAdd?.Invoke(this, new FileWatcherEventArgs() { FileChange = info, IsOnAddList = true, RootPath = root, FileName = fileName, });
                filesWatchList.Add(info);
            }
            else
            {
                OnNotFound?.Invoke(this, new FileWatcherEventArgs() { RootPath = root, FileName = fileName, });
            }
        }

        //public void CancelWatcher()
        //{
        //    source.SetCanceled();
        //}

        private void AddWatcherList()
        {
            foreach (var file in filesWatchList) {
                IChangeToken token = fileProvider.Watch(file.FullName);
                token.RegisterChangeCallback(state => {
                    var o = ((TaskCompletionSource<string>)state);
                    o.TrySetResult(file.FullName);
                }, source);
            }
        }

        public void Wait(bool continueWatch)
        {
            AddWatcherList();
            string r = source.Task.GetAwaiter().GetResult();
            OnChange?.Invoke(this, new FileWatcherEventArgs() { FileChange = new FileInfo(r), RootPath = root, FileName = r, });
            while (continueWatch && !source.Task.IsCanceled) {
                AddWatcherList();
                r = source.Task.GetAwaiter().GetResult();
                OnChange?.Invoke(this, new FileWatcherEventArgs() { FileChange = new FileInfo(r), RootPath = root, FileName = r, });
            }
        }
    }
}
