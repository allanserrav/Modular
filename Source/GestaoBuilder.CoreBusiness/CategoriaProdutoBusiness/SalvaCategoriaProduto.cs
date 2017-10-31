using System;
using System.Collections.Generic;
using System.Text;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.Business.Domain;
using GestaoBuilder.Shared.Data.System.Domain;

namespace GestaoBuilder.CoreBusiness.CategoriaProdutoBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.SalvaCategoriaProduto, Nome = "Salvar dados da categoria de produto")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class SalvaCategoriaProduto : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            if (entry.ResultadoAnterior.IsValidacaoError) {
                return entry.ResultadoAnterior;
            }
            var categoria = entry.Parse<CategoriaProduto>(Mapper).Result;
            var repository = Support.GetCategoriaProdutoRepository();
            repository.Salvar(categoria);
            return Support.GetSucessResult(categoria);
        }
    }
}
