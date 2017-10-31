using System;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.Business.Domain;
using GestaoBuilder.Shared.Data.System.Domain;

namespace GestaoBuilder.CoreBusiness.ProdutoBusiness
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
