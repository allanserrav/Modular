using FluentValidation;
using Modular.Shared.Data.Business.Domain;

namespace Modular.CoreBusiness.VendaBusiness.Validators
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