using System;

namespace Modular.Shared.Data
{
    public interface IMapper
    {
        IDataMap Get(string document);

        IDataMap Get<TData>();
        IDataMap Get(Type type);
    }
}