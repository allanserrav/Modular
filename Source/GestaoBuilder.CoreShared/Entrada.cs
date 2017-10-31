using GestaoBuilder.CoreShared.Bases;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.System.Domain;
using GestaoBuilder.Shared.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace GestaoBuilder.CoreShared
{
    public class Entrada : BaseJsonParse, IEntrada
    {
        public Entrada(JToken jsonoriginal, IUsuario usuarioEntry, TipoOperacaoEnum tipo)
        {
            JsonOriginalEntry = jsonoriginal;
            UsuarioEntry = usuarioEntry;
            TipoOperacao = tipo;
            //CodigoOperacao = codigoOperacao;
        }

        public Entrada(IResultado resultado, JToken jsonoriginal, IUsuario usuarioEntry, TipoOperacaoEnum tipo)
        {
            JsonOriginalEntry = jsonoriginal;
            //ValorAtual = resultado.EntradaTratada ?? resultado.ResultValue;
            ValorAtual = resultado.ResultValue;
            UsuarioEntry = usuarioEntry;
            TipoOperacao = tipo;
            //CodigoOperacao = codigoOperacao;
            ResultadoAnterior = resultado;
        }

        public IUsuario UsuarioEntry { get; }
        public TipoMessageEnum Tipo { get; }
        public JToken JsonOriginalEntry { get; }
        public IResultado ResultadoAnterior { get; }
        public string CodigoOperacao { get; }
        public TipoOperacaoEnum TipoOperacao { get; }
        public object ValorAtual { get; set; }

        public override IParseResult<T> Parse<T>(IContractResolver resolver, bool throwOnParseError = true, params JsonConverter[] converters)
        {
            if (ValorAtual != null && ValorAtual.InheritedFrom(typeof(T))) {
                return new ParseResult<T> { IsParseSucess = true, Result = (T)ValorAtual };
            }
            var parseResult = DoParse<T>(JsonOriginalEntry, resolver, throwOnParseError, converters);
            if (parseResult.Result.InheritedFrom(ValorAtual))
            {
                parseResult.Result.Join(ValorAtual);
            }
            return parseResult;
        }
    }

}
