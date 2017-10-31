using System;

namespace GestaoBuilder.Shared.Data
{
    public interface IDataTransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
