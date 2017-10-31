using GestaoBuilder.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Data.Business.Domain;

namespace GestaoBuilder.CoreBusiness.CategoriaProdutoBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.ListaCategoriaProduto, Nome = "Listar categorias de produto")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class ConsultaCategoriaProduto : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            if(entry.ResultadoAnterior != null && entry.ResultadoAnterior.IsValidacaoError)
            {
                return entry.ResultadoAnterior;
            }
            var categoriaEntry = entry.Parse<CategoriaProduto>(Mapper).Result;
            var repository = Support.GetCategoriaProdutoRepository();
            if (!String.IsNullOrEmpty(categoriaEntry.Id))
            {
                var responseFromId = repository.Get(categoriaEntry.Id);
                return Support.GetSucessResult(responseFromId);
            }
            var response = repository.FindAll();
            return Support.GetSucessResult(response);
        }
    }
}
