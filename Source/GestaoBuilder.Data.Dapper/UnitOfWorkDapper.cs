using GestaoBuilder.Shared.Contracts;
using System;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.System;

namespace GestaoBuilder.Data.Dapper
{
    public class UnitOfWorkDapper : IUnitOfWork
    {
        public UnitOfWorkDapper(IDataRead<IDataSysKey> reader)
        {
            this.ReadContext = reader;
        }

        //public JToken
        public IDataRead<IDataSysKey> ReadContext { get; }

        public IDataWrite<IDataSysKey> WriteContext => throw new NotImplementedException();

        public bool HasTransaction => throw new NotImplementedException();

        public IDataTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
