using GestaoBuilder.Shared.Data.System;

namespace GestaoBuilder.Shared.Data
{
    public interface IUnitOfWork
    {
        IDataRead<IDataSysKey> ReadContext { get; }

        IDataWrite<IDataSysKey> WriteContext { get; }

        IDataTransaction BeginTransaction();

        bool HasTransaction { get; }
    }

}
