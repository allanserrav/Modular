using System;
using System.IO;

namespace GestaoBuilder.Shared.Helpers
{
    public static class FileHelper
    {
        public static string GetTextFromFilePath(string filepath)
        {
            var file = new FileInfo(filepath);
            if(file.Exists)
            {
                var reader = file.OpenText();
                return reader.ReadToEnd();
            }
            return String.Empty;
        }
    }
}
