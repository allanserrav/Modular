using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Modular.CoreBusiness.ClienteBusiness.Entries;

namespace Modular.CoreBusiness.ClienteBusiness.Validators
{
    public class ClienteEntryValidator : AbstractValidator<ClienteEntry>
    {
        public ClienteEntryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(c => c.ClienteCodigo).NotEmpty().WithMessage("Código não informado");
            RuleFor(c => c.Pontuacao).GreaterThan(0).WithMessage("Pontuação acima de zero deve ser informada");
        }
    }
}
