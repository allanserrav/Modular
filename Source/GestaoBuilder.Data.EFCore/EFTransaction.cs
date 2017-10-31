using GestaoBuilder.Shared.Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using GestaoBuilder.Shared.Data;

namespace GestaoBuilder.Data.EFCore
{
    public class EFTransaction : IDataTransaction
    {
        private readonly IDbContextTransaction tr;
        private readonly bool noeffect;
        private bool iscommited;

        public event EventHandler<EventArgs> OnCommit;
        public event EventHandler<EventArgs> OnRollback;

        public EFTransaction(IDbContextTransaction tr, bool noeffect = false)
        {
            this.tr = tr;
            this.noeffect = noeffect;
        }


        public void Commit()
        {
            OnCommit?.Invoke(this, EventArgs.Empty);
            if(!noeffect) tr.Commit();
            iscommited = true;
        }

        public void Dispose()
        {
            if(!iscommited) {
                Rollback();
            }
            tr.Dispose();
        }

        public void Rollback()
        {
            OnRollback?.Invoke(this, EventArgs.Empty);
            if(!noeffect) tr.Rollback();
        }
    }
}
