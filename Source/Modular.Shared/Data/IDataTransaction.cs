using System;

namespace Modular.Shared.Data
{
    public interface IDataTransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
