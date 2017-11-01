using Modular.CoreBusiness.Bases;
using Modular.Shared.Data.Business.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Modular.Shared.Contracts;
using FluentValidation;

namespace Modular.CoreBusiness.CategoriaProdutoBusiness.Validators
{
    public class CategoriaProdutoValidator : BaseValidator<CategoriaProduto>
    {
        public CategoriaProdutoValidator(TipoOperacaoEnum tipoOperacao) : base(tipoOperacao)
        {
            RuleFor(i => i.Id)
                .NotEmpty()
                .When(i => tipoOperacao == TipoOperacaoEnum.Atualizacao)
                .WithMessage("Identificador não informado");
        }
    }
}
