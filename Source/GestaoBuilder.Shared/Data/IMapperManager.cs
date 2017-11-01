using System;
using Modular.Shared.Data.Business;

namespace Modular.Shared.Data
{
    public interface IMapperManager : IMapper
    {
        IMapperManager AddInheritance<TData, TInherit>(Action<IDataMap<TData>> domapper)
            where TData : IDataBis, TInherit
            where TInherit : IDataBis;
        IMapperManager AddMap<TData>(Action<IDataMap<TData>> domapper) where TData : IDataBis;
        IMapperManager AddMapObject<TData>(Action<IDataMap<TData>> domapper) where TData : class ;
        IMapperManager AddMap<TData>(string document, Action<IDataMap<TData>> domapper) where TData : IDataBis;
        IMapperManager AddMapParse<TData>(Action<IDataMapParse<TData>> domapper);
    }
}