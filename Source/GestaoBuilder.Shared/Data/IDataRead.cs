using System.Linq;
using Modular.Shared.Data.System;

namespace Modular.Shared.Data
{
    public interface IDataRead<in TDataKey>
    {
        TEntity Get<TEntity>(TDataKey key)
            where TEntity : class ,IBaseData, TDataKey;

        TEntity GetByCodigo<TEntity>(string codigo)
            where TEntity : class, IBaseData, TDataKey;

        IQueryable<TEntity> GetQuery<TEntity>()
            where TEntity : class, IBaseData, TDataKey;
    }
}
