using System;
using System.Collections.Generic;
using Modular.Shared.Data;
using Modular.Shared.Data.Business;

namespace Modular.Core
{
    public sealed class DataMapperManager : IMapperManager
    {
        private readonly Dictionary<string, IDataMap> _mappers = new Dictionary<string, IDataMap>();
        public IMapperManager AddInheritance<TData, TInherit>(Action<IDataMap<TData>> domapper) where TData : IDataBis, TInherit where TInherit : IDataBis
        {
            var mapperInherit = (DataMap<TInherit>)Get<TInherit>();
            var mapper = DataMap<TData>.Inialize("");
            domapper(mapper);
            mapperInherit.Join(mapper);
            return this;
        }

        public IMapperManager AddMap<TData>(Action<IDataMap<TData>> domapper) where TData : IDataBis
        {
            string document = typeof(TData).GetDocumentName();
            return AddMap(document, domapper);
        }

        public IMapperManager AddMapObject<TData>(Action<IDataMap<TData>> domapper) where TData : class
        {
            var type = typeof(TData);
            string document = type.GetDocumentName();
            document = document ?? type.BaseType.GetDocumentName();
            document = document ?? type.FullName;
            var mapper = DataMap<TData>.Inialize(document);
            domapper(mapper);
            _mappers.Add(document, mapper);
            return this;
        }

        public IMapperManager AddMap<TData>(string document, Action<IDataMap<TData>> domapper)
            where TData : IDataBis
        {
            var mapper = DataMap<TData>.Inialize(document);
            domapper(mapper);
            _mappers.Add(document, mapper);
            return this;
        }

        public IMapperManager AddMapParse<TData>(Action<IDataMapParse<TData>> domapper)
        {
            var type = typeof(TData);
            string document = type.GetDocumentName();
            document = document ?? type.BaseType.GetDocumentName();
            document = document ?? type.FullName;
            var mapper = DataMap<TData>.Inialize(document);
            domapper(mapper);
            _mappers.Add(document, mapper);
            return this;
        }

        public IDataMap Get(string document)
        {
            if (_mappers.ContainsKey(document))
            {
                return _mappers[document];
            }
            return default(IDataMap);
        }

        public IDataMap Get<TData>()
        {
            return Get(typeof(TData));
        }

        public IDataMap Get(Type type)
        {
            string document = type.GetDocumentName();
            document = document ?? type.BaseType.GetDocumentName();
            document = document ?? type.FullName;
            return Get(document);
        }
    }
}
