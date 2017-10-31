using GestaoBuilder.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestaoBuilder.CoreShared.Model;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.System;

namespace GestaoBuilder.Data.EFCore
{
    public static class IncludeAll
    {
        public static IQueryable<TEntity> Resolve<TEntity>(DbSet<TEntity> set)
            where TEntity : class, IBaseData, IDataSysKey
        {
            if (typeof(TEntity) == typeof(Modulo)) {
                return set.OfType<Modulo>()
                        .Include(a => a.AgrupamentoOrdenacaoIn)
                            .ThenInclude(a => a.ModuloExecutor)
                        .OfType<TEntity>();
            }
            return set;
        }
    }
}
