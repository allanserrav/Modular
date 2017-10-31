using System;

namespace GestaoBuilder.Shared.Contracts
{
    public interface IElapsedTimeWatch
    {
        TimeSpan Elapsed { get; }

        void WaitFor(TimeSpan time);
    }
}
