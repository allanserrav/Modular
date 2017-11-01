using System;
using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.System.Domain;

namespace Modular.CoreBusiness.ProdutoBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.ListaProduto, Nome = "Listar os produtos")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class ListaProduto : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            var filtro = entry.Parse<Produto>(Mapper).Result;
            var repository = Support.GetProdutoRepository();
            var p = repository.GetByCodigo(filtro.Codigo);

            return Support.GetSucessResult(p);
        }
    }
}
