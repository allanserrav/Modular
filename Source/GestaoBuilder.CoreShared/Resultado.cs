using System;
using System.Collections.Generic;
using GestaoBuilder.CoreShared.Bases;
using GestaoBuilder.Shared.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace GestaoBuilder.CoreShared
{
    public class Resultado : Resultado<Resultado>
    {
    }

    public class Resultado<TResult> : BaseJsonParse, IResultado
        where TResult : Resultado<TResult>, new()
    {
        public bool IsException { get; set; }
        public bool HasError => IsEntryParserError || IsValidacaoError || IsBusinessError || IsException;
        public string ResultMessage { get; set; }
        public ResultadoMessage[] Messages { get; set; }
        //public string ResultMessageDetail { get; set; }
        public TimeSpan ExecutionTime { get; set; }
        public JToken JsonFromOriginal { get; protected set; }
        public object ResultValue { get; set; }
        public JsonConverter[] ResultValueConverters { get; set; }
        public IContractResolver ResultValueResolver { get; set; }
        //public object EntradaTratada { get; protected set; }
        public bool IsEntryParserError { get; set; }
        public bool IsValidacaoError { get; set; }
        public bool IsBusinessError { get; set; }

        //public static IResultado Load(IResultado values, bool isSucess, string message, Exception ex)
        //{
        //    if (ex != null) {
        //        return Exception(message, ex);
        //    }
        //    return isSucess ? Sucess(values) : Error(message);
        //}

        public static Resultado Error(string messageErro, IResultado inner)
        {
            List<ResultadoMessage> messages = new List<ResultadoMessage>
            {
                new ResultadoMessage {ChaveMessage = "", Message = messageErro, TipoMessage = TipoMessageEnum.Business}
            };
            messages.AddRange(inner.Messages);
            var result = new Resultado {
                IsException = inner.IsException,
                IsEntryParserError = inner.IsEntryParserError,
                IsValidacaoError = inner.IsValidacaoError,
                IsBusinessError = inner.IsBusinessError,
                ResultValue = inner.ResultValue,
                Messages = messages.ToArray()
            };
            if (inner is Resultado resultadoInner)
            {
                result.ResultValueResolver = resultadoInner.ResultValueResolver;
                result.ResultValueConverters = resultadoInner.ResultValueConverters;
            }
            return result;
        }

        public static IResultado Exception(string messageErro, Exception ex)
        {
            Guid exCode = Guid.NewGuid();
            var result = new Resultado {
                IsException = true,
                ResultMessage = messageErro,
                Messages = new[] { new ResultadoMessage { ChaveMessage = exCode.ToString(), Message = ex.Message, TipoMessage = TipoMessageEnum.Exception, } }, // Chave para mensagem de exceção
            };
            return result;
        }

        public static IResultado Exception(Exception ex, out Guid exCode)
        {
            exCode = Guid.NewGuid();
            var result = new Resultado {
                IsException = true,
                Messages = new[] { new ResultadoMessage { ChaveMessage = exCode.ToString(), Message = ex.Message, TipoMessage = TipoMessageEnum.Exception, } }, // Chave para mensagem de exceção
            };
            return result;
        }

        //public static IResultado Sucess<TValue>(TValue resultValue, IContractResolver resolver = null, params JsonConverter[] converters)
        //{
        //    var result = new Resultado {
        //        ResultValue = resultValue,
        //        ResultValueResolver = resolver,
        //        ResultValueConverters = converters
        //    };
        //    //result.From(resultValue);
        //    return result;
        //}

        //public static IResultado Sucess(IResultado values)
        //{
        //    var resultadoEntry = (Resultado)values;
        //    var result = new Resultado {
        //        IsException = true,
        //        JsonFromOriginal = resultadoEntry.JsonFromOriginal,
        //        ResultValue = resultadoEntry.ResultValue,
        //        ResultValueConverters = resultadoEntry.ResultValueConverters,
        //        ResultValueResolver = resultadoEntry.ResultValueResolver,
        //        EntradaTratada = resultadoEntry.EntradaTratada,
        //    };
        //    return result;
        //}

        //public IResultado SetEntradaTratada(object entradaTratada)
        //{
        //    this.EntradaTratada = entradaTratada;
        //    return this;
        //}

        public JToken ResultJson()
        {
            if (ResultValue.GetType() == typeof(JToken)) {
                return (JToken)ResultValue;
            }
            var serializer = new JsonSerializer() {
                ContractResolver = ResultValueResolver,
            };
            foreach (var converter in ResultValueConverters) {
                serializer.Converters.Add(converter);
            }
            return JToken.FromObject(ResultValue, serializer);
        }

        public override IParseResult<T> Parse<T>(IContractResolver resolver, bool throwOnParseError = true, params JsonConverter[] converters)
        {
            if (typeof(T) == ResultValue.GetType()) {
                return new ParseResult<T>() { IsParseSucess = true, Result = (T)ResultValue };
            }
            return DoParse<T>(JsonFromOriginal, resolver, throwOnParseError, converters);
        }
    }
}
