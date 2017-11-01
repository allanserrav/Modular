using System;
using FluentValidation;
using Modular.Shared.Data.Business.Domain;

namespace Modular.CoreBusiness.VendaBusiness.Validators
{
    public class ItemVendaValidator : AbstractValidator<ItemVenda>
    {
        public ItemVendaValidator()
        {
            RuleFor(i => i.Produto)
                .NotNull()
                .WithMessage("Produto não informado");
            RuleFor(i => i.QuantidadePedida)
                .GreaterThan(0)
                .When(i => i.Produto.Classe == ClasseProduto.Estoque)
                .WithMessage("Quantidade pedida não informada");
            RuleFor(c => c.Desconto.ValorFixo)
                .GreaterThan(0)
                .When(i => i.Desconto != null)
                .WithMessage("Valor de desconto informado inválido");
        }

        private void Action()
        {
            throw new NotImplementedException();
        }
    }
}