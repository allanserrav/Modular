﻿using FluentValidation;
using GestaoBuilder.Shared.Data.Business.Domain;

namespace GestaoBuilder.CoreBusiness.VendaBusiness.Validators
{
    public class VendaValidator : AbstractValidator<Venda>
    {
        public VendaValidator()
        {
            //CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(c => c.Cliente).NotNull().WithMessage("Cliente não informado");
            RuleFor(c => c.Itens).NotEmpty().WithMessage("Nenhum item informado");
            RuleFor(c => c.Itens).SetCollectionValidator(new ItemVendaValidator());
        }
    }
}