using Modular.ClienteBusinessTeste.Domain;
using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.System.Domain;
using Newtonsoft.Json;

namespace Modular.ClienteBusinessTeste
{
    [ModuloInfo(Codigo = "ABC102", Nome = "Ajustar o produto")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class AjustaProduto : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry, IModulo modulo)
        {
            var mapper = Mapper.Get<ProdutoAjustado>();
            var contractResolver = Support.GetContractResolver(mapper);
            var ajustamento = entry.Parse<ProdutoAjustado>(contractResolver).Result;
            ajustamento.Ajuste = "Teste ajustamento";
            return Support.GetSucessResult(ajustamento);
        }
    }
}