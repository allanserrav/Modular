using FluentValidation;
using Modular.Shared.Data.Business.Domain;

namespace Modular.CoreBusiness.ProdutoBusiness.Validators
{
    public class ProdutoValidator : AbstractValidator<Produto>
    {
        public ProdutoValidator()
        {
            //CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(c => c.Nome).NotEmpty().WithMessage("Nome do produto não informado");
            RuleFor(c => c.Categoria).NotNull().WithMessage("Categoria não informada ou inválida");
        }
    }
}