using Modular.Shared.Data.System;

namespace Modular.Shared.Data
{
    public interface IUnitOfWork
    {
        IDataRead<IDataSysKey> ReadContext { get; }

        IDataWrite<IDataSysKey> WriteContext { get; }

        IDataTransaction BeginTransaction();

        bool HasTransaction { get; }
    }

}
