using Modular.Shared;
using Modular.Shared.Data.System.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Modular.Shared.Contracts
{
    public enum TipoOperacaoEnum
    {
        Novo = 1,
        Atualizacao,
        Query,
    }

    public interface IEntrada : IJsonParse
    {
        IUsuario UsuarioEntry { get; }

        JToken JsonOriginalEntry { get; }

        IResultado ResultadoAnterior { get; }

        string CodigoOperacao { get; }

        TipoOperacaoEnum TipoOperacao { get; }
    }
}
