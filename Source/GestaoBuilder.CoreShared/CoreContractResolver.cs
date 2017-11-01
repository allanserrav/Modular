using System;
using System.Collections.Generic;
using System.Linq;
using Modular.Shared.Data;
using Modular.Shared.Data.Business;
using Newtonsoft.Json.Serialization;

namespace Modular.CoreShared
{
    public class CoreContractResolver : DefaultContractResolver
    {
        public IMapper Mapper { get; }
        public IDataMap CurrentMap { get; private set; }

        public CoreContractResolver(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected virtual string CustomResolvePropertyName(string propertyName)
        {
            var itemMap = CurrentMap.ParseMaps.FirstOrDefault(d => d.PropertyName.Equals(propertyName) && !d.IsIgnore);
            //bool isResolved = this.PropertyMappings.TryGetValue(propertyName, out var resolvedName);
            return (itemMap != null) ? itemMap.EntryRefName : base.ResolvePropertyName(propertyName);
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            return CustomResolvePropertyName(propertyName);
        }

        protected override JsonContract CreateContract(Type objectType)
        {
            CurrentMap = Mapper.Get(objectType);
            return base.CreateContract(objectType);
        }
    }
}
