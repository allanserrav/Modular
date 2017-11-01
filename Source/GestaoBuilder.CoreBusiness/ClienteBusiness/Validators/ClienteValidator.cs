using System;
using FluentValidation;
using Modular.Shared.Data.Business.Domain;

namespace Modular.CoreBusiness.ClienteBusiness.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            //CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(c => c.Nome).NotEmpty().WithMessage("Nome não informado");
            RuleFor(c => c.Apelido).NotEmpty().WithMessage("Apelido não informado");
            RuleFor(c => c.Nascimento).GreaterThanOrEqualTo(new DateTime(1980, 1, 1)).WithMessage("Nascimento inválido");
            RuleFor(c => c.Email).EmailAddress().WithMessage("Email informado inválido");
            /*
            ^ - Início da string.
            \( - Um abre parênteses.
            [1-9]{2} - Dois dígitos de 1 a 9. Não existem códigos de DDD com o dígito 0.
            \) - Um fecha parênteses.
              - Um espaço em branco.
            [2-9] - O primeiro dígito. Nunca será 0 ou 1.
            [0-9]{3,4} - Os demais dígitos da primeira metade do número do telefone, perfazendo um total de 4 ou 5 dígitos na primeira metade.
            \- - Um hífen.
            [0-9]{4} - A segunda metade do número do telefone.
            $ - Final da string.
            */
            RuleFor(c => c.Telefone).Matches("^\\([1-9]{2}\\) [2-9][0-9]{3,4}\\-[0-9]{4}$").WithMessage("Telefone informado inválido");
        }

    }
}