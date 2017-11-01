using System;
using System.Collections.Generic;
using System.Text;
using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.System.Domain;

namespace Modular.CoreBusiness.CategoriaProdutoBusiness
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
