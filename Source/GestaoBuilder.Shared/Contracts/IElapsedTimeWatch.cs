using System;

namespace Modular.Shared.Contracts
{
    public interface IElapsedTimeWatch
    {
        TimeSpan Elapsed { get; }

        void WaitFor(TimeSpan time);
    }
}
