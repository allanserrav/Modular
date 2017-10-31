using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using GestaoBuilder.Shared.Data;

namespace GestaoBuilder.Shared.Contracts
{
    public interface IParseResult<out T>
    {
        bool IsParseSucess { get; }

        T Result { get; }
    }

    public interface IJsonParse
    {
        IParseResult<T> Parse<T>(IContractResolver resolver, bool throwOnParseError = true, params JsonConverter[] converters);
        IParseResult<T> Parse<T>(IContractResolver resolver, params JsonConverter[] converters);
        IParseResult<T> Parse<T>(IMapper mappers, params JsonConverter[] converters);
        IParseResult<T> Parse<T>(bool throwOnParseError = true, params JsonConverter[] converters);
        IParseResult<T> Parse<T>(params JsonConverter[] converters);
    }
}
