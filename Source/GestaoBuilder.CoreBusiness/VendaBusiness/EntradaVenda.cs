using GestaoBuilder.CoreBusiness.VendaBusiness.Validators;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.Business.Domain;
using GestaoBuilder.Shared.Data.System.Domain;

namespace GestaoBuilder.CoreBusiness.VendaBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.EntradaVenda, Nome = "Entrada e validação dos dados",
        AgrupamentoCodigo = BusinessModuloCodigo.AgrupamentoRealizaVenda, AgrupamentoOrdem = 1)]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class EntradaVenda : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            var parsere = entry.Parse<Venda>(Mapper);
            if (!parsere.IsParseSucess) return Support.GetEntryParserResultError("Erro no formato de entrada para venda");
            var vendaEntry = parsere.Result;
            var clienteRepository = Support.GetClienteRepository();
            var produtoRepository = Support.GetProdutoRepository();
            vendaEntry.Cliente = clienteRepository.GetByCodigo(vendaEntry.Cliente.Codigo);
            foreach (var itemVenda in vendaEntry.Itens)
            {
                itemVenda.Produto = produtoRepository.GetByCodigo(itemVenda.Produto.Codigo);
                itemVenda.ValorDeVenda = itemVenda.ValorDeVenda <= 0 ? itemVenda.Produto.Preco : itemVenda.ValorDeVenda;
                itemVenda.QuantidadePedida =
                    itemVenda.Produto.Classe == ClasseProduto.Servico ? 1 : itemVenda.QuantidadePedida;
            }

            var validator = new VendaValidator();
            var resultadoValidacao = validator.Validate(vendaEntry);
            if (!resultadoValidacao.IsValid)
            {
                var messages = resultadoValidacao.Errors.GetValidacaoMessage();
                return Support.GetValidacaoResultError(messages, vendaEntry);
            }
            return Support.GetSucessResult(vendaEntry);
        }
    }
}