using GestaoBuilder.ClienteBusinessTeste.Domain;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.Business.Domain;
using GestaoBuilder.Shared.Data.System.Domain;
using Newtonsoft.Json;

namespace GestaoBuilder.ClienteBusinessTeste
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