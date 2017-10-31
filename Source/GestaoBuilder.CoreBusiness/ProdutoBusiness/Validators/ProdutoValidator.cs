using FluentValidation;
using GestaoBuilder.Shared.Data.Business.Domain;

namespace GestaoBuilder.CoreBusiness.ProdutoBusiness.Validators
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