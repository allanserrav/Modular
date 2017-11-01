using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data.Business.Domain;

namespace Modular.CoreBusiness.ProdutoBusiness
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