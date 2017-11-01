using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

namespace Modular.CoreShared.Bases
{
    public abstract class BaseContractResolver : DefaultContractResolver
    {
        protected Dictionary<string, object> PropertyMappings { get; private set; }

        protected virtual string CustomResolvePropertyName(string propertyName)
        {
            bool isResolved = this.PropertyMappings.TryGetValue(propertyName, out var resolvedName);
            return (isResolved) ? resolvedName.ToString() : base.ResolvePropertyName(propertyName);
        }

        protected abstract Dictionary<string, object> ProcessarMappings();

        protected override string ResolvePropertyName(string propertyName)
        {
            if(PropertyMappings == null || PropertyMappings.Count > 0) {
                PropertyMappings = ProcessarMappings();
            }
            return CustomResolvePropertyName(propertyName);
        }

        //protected override JsonContract CreateContract(Type objectType)
        //{
        //    if (objectType == typeof(Parametro)) {
        //        ParametroContractResolver resolver = new ParametroContractResolver();
        //        return resolver.GetContract(objectType);
        //    }
        //    return base.CreateContract(objectType);
        //}
    }
}
