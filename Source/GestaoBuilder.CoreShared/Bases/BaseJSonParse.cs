using System;
using Modular.Shared;
using Modular.Shared.Contracts;
using Modular.Shared.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Modular.CoreShared.Bases
{
    public class ParseException : ApplicationException
    {
        public ParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    public abstract class BaseJsonParse : IJsonParse
    {
        public abstract IParseResult<T> Parse<T>(IContractResolver resolver, bool throwOnParseError = true, params JsonConverter[] converters);
        public IParseResult<T> Parse<T>(IContractResolver resolver, params JsonConverter[] converters)
        {
            return Parse<T>(resolver, true, converters);
        }

        public IParseResult<T> Parse<T>(IMapper mappers, params JsonConverter[] converters)
        {
            var contract = new CoreContractResolver(mappers);
            return Parse<T>(contract, true, converters);
        }

        public IParseResult<T> Parse<T>(bool throwOnParseError = true)
        {
            return Parse<T>(null, throwOnParseError, Array.Empty<JsonConverter>());
        }

        public IParseResult<T> Parse<T>(bool throwOnParseError = true, params JsonConverter[] converters)
        {
            return Parse<T>(null, throwOnParseError, converters);
        }

        public IParseResult<T> Parse<T>(params JsonConverter[] converters)
        {
            return Parse<T>(null, true, converters);
        }

        protected ParseResult<T> DoParse<T>(JToken entry, IContractResolver resolver = null, bool throwOnParseError = true, params JsonConverter[] converters)
        {
            if (typeof(T) == typeof(JToken)) {
                return new ParseResult<T>() { IsParseSucess = true, Result = (T)(object)entry };
            }
            var serializer = new JsonSerializer {
                ContractResolver = resolver
            };
            foreach (var converter in converters) {
                serializer.Converters.Add(converter);
            }
            try
            {
                var obj = entry.ToObject<T>(serializer);
                if (obj is IDataModificada modi)
                {
                    modi.LimparModificados();
                }
                return new ParseResult<T> {IsParseSucess = true, Result = obj};
            }
            catch(Exception ex)
            {
                if (throwOnParseError) throw new ParseException("Entrada inválida", ex);
                return new ParseResult<T> { IsParseSucess = false };
            }
        }
    }
}
