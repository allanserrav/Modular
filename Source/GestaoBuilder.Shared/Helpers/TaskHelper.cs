using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Modular.Shared.Contracts;

namespace Modular.Shared.Helpers
{
    public static class TaskHelper
    {
        public static async Task<IResultado> MeasureExecutionTimeAsync(Func<IResultado> action)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = await Task.Run(action);

            stopwatch.Stop();

            //Console.WriteLine(stopwatch.ElapsedMilliseconds + " ms");
            return result;
        }
    }
}
