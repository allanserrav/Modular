using Modular.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Modular.CoreShared.Model;
using Modular.Shared.Data;
using Modular.Shared.Data.System;

namespace Modular.Data.EFCore
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
