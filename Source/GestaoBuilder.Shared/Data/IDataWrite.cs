using System.Threading.Tasks;

namespace GestaoBuilder.Shared.Data
{
    public interface IDataWrite<in TDataKey>
        where TDataKey : IDataKey
    {
        void Add<TEntity>(TEntity entity)
            where TEntity : class, IBaseData, TDataKey;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class, IBaseData, TDataKey;

        void Update<TEntity>(TEntity entity)
            where TEntity : class, IBaseData, TDataKey;
    }
}
