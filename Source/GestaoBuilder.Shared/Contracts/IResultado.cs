using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace GestaoBuilder.Shared.Contracts
{
    public enum TipoMessageEnum
    {
        Parse = 1,
        Validacao,
        Business,
        Exception,
    }

    public class ResultadoMessage
    {
        public string Message { get; set; }
        public string ChaveMessage { get; set; }
        public TipoMessageEnum TipoMessage { get; set; }
    }

    public interface IResultado : IJsonParse
    {
        //JToken ResultValue { get; }
        //JToken From<T>(T value, params JsonConverter[] converters);
        string ResultMessage { get; }
        ResultadoMessage[] Messages { get; }
        //string ResultMessageDetail { get; }
        object ResultValue { get; }
        //object EntradaTratada { get; }

        /// <summary>
        /// Erro no parse
        /// </summary>
        bool IsEntryParserError { get; }
        /// <summary>
        /// Erro na validação
        /// </summary>
        bool IsValidacaoError { get; }
        /// <summary>
        /// Erro de negocio
        /// </summary>
        bool IsBusinessError { get; }
        /// <summary>
        /// Erro processando - foi gerada uma exceção que parou a execução
        /// </summary>
        bool IsException { get; }
        /// <summary>
        /// Indica que houve algum erro
        /// </summary>
        bool HasError { get; }

        JToken ResultJson();
    }
}
