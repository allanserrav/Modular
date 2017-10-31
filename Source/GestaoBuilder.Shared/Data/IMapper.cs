using System;

namespace GestaoBuilder.Shared.Data
{
    public interface IMapper
    {
        IDataMap Get(string document);

        IDataMap Get<TData>();
        IDataMap Get(Type type);
    }
}