using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GestaoBuilder.Shared.Contracts;

namespace GestaoBuilder.CoreShared
{

    public class Timewatch : IElapsedTimeWatch, IDisposable
    {
        private readonly Stopwatch watch;
        public Action<TimeSpan> OnWaitFor;

        public Timewatch()
        {
            watch = new Stopwatch();
            Start();
        }

        public void Start() => watch.Start();

        public TimeSpan Elapsed => watch.Elapsed;

        public void Stop() => watch.Stop();

        public void Dispose()
        {
            Stop();
        }

        public void WaitFor(TimeSpan time)
        {
            var t = Task.Delay(time);
            t.GetAwaiter().GetResult();

            OnWaitFor?.Invoke(time);
        }

    }
}
