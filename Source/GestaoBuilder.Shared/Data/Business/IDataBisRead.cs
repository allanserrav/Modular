using System.Collections.Generic;
using System.Linq;
using GestaoBuilder.Shared.Contracts;

namespace GestaoBuilder.Shared.Data.Business
{
    public interface IDataBisRead : IDataRead<IDataBisKey>
    {
        IEnumerable<TEntity> GetJsonQuery<TEntity>(string jsonquery, string jsonorder = null)
            where TEntity : class, IBaseData, IDataBisKey;

        TEntity Get<TEntity>(string id)
            where TEntity : class, IBaseData, IDataBisKey;
    }
}