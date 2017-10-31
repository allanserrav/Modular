using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.Business.Domain;

namespace GestaoBuilder.CoreBusiness.ProdutoBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.SalvaProduto, Nome = "Salvar o produto")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class SalvaProduto : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            var produto = entry.Parse<Produto>(Mapper).Result;
            var repository = Support.GetProdutoRepository();
            repository.Salvar(produto);
            return Support.GetSucessResult(produto);
        }
    }
}