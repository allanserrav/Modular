using GestaoBuilder.CoreBusiness.Bases;
using GestaoBuilder.Shared.Data.Business.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using GestaoBuilder.Shared.Contracts;
using FluentValidation;

namespace GestaoBuilder.CoreBusiness.CategoriaProdutoBusiness.Validators
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
