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
    [Modulo(Codigo = BusinessModuloCodigo.ValidacaoCategoriaProduto, Nome = "Validação de dados da categoria de produto")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class ValidacaoCategoriaProduto : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            var categoria = entry.Parse<CategoriaProduto>(Mapper).Result;
            if (String.IsNullOrEmpty(categoria.Codigo))
            {
                return Support.GetValidacaoResultError(BusinessHelper.GetValidacaoMessage("O código não foi informado"), categoria);
            }
            if (String.IsNullOrEmpty(categoria.Descricao))
            {
                return Support.GetValidacaoResultError(BusinessHelper.GetValidacaoMessage("Categoria sem o nome"), categoria);
            }
            return Support.GetSucessResult(categoria);
        }
    }
}
