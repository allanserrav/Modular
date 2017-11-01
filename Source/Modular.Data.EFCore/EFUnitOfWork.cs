using Modular.Shared.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using Modular.Shared.Data;
using Modular.Shared.Data.System;

namespace Modular.Data.EFCore
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly EFDataContext dbcontext;
        private IDbContextTransaction currentTr;

        public EFUnitOfWork(DbContextOptions<EFDataContext> options, IDataRead<IDataSysKey> reader)
        {
            this.ReadContext = reader;
            if (reader is EFDataContext) {
                dbcontext = (EFDataContext)reader;
            }
            else {
                dbcontext = new EFDataContext(options);
            }
        }

        public IDataRead<IDataSysKey> ReadContext { get; }

        public IDataWrite<IDataSysKey> WriteContext => dbcontext;

        public bool HasTransaction { get; private set; }

        public IDataTransaction BeginTransaction()
        {

            currentTr = !HasTransaction ? dbcontext.Database.BeginTransaction() : currentTr;
            var entitytr = new EFTransaction(currentTr, HasTransaction);
            HasTransaction = true;
            entitytr.OnCommit += OnFinishTransaction;
            entitytr.OnRollback += OnFinishTransaction;
            return entitytr;
        }

        void OnFinishTransaction(object sender, EventArgs e)
        {
            HasTransaction = false;
        }
    }
}
