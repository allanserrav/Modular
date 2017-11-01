using FluentValidation;
using Modular.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modular.CoreBusiness.Bases
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        protected readonly TipoOperacaoEnum tipoOperacao;

        public BaseValidator(TipoOperacaoEnum tipoOperacao)
        {
            this.tipoOperacao = tipoOperacao;
        }
    }
}
