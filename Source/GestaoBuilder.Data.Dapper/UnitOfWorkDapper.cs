using Modular.Shared.Contracts;
using System;
using Modular.Shared.Data;
using Modular.Shared.Data.System;

namespace Modular.Data.Dapper
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
