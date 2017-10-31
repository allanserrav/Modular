using System.Linq;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.Business.Domain;
using GestaoBuilder.Shared.Data.System.Domain;

namespace GestaoBuilder.CoreBusiness.VendaBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.RealizaVenda, Nome = "Realizar a venda dos produtos",
            AgrupamentoCodigo = BusinessModuloCodigo.AgrupamentoRealizaVenda, AgrupamentoOrdem = 2, PrincipalNoAgrupamento = true)]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class RealizaVenda : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            if (entry.ResultadoAnterior.IsValidacaoError)
            {
                return entry.ResultadoAnterior;
            }
            var vendaEntry = entry.Parse<Venda>(Mapper).Result;
            if (vendaEntry.Itens.Any(i => i.Produto.Classe == ClasseProduto.Estoque)) // Se tiver algum produto de estoque
            {
                return Support.GetBusinessResultError("A configuração atual do serviço de venda não permite itens de estoque", vendaEntry);
            } 

            // Calcular valor da venda
            vendaEntry.ValorTotalSemCalculo = vendaEntry.Itens.Sum(i => i.ValorDeVenda);
            vendaEntry.ValorTotalCalculado = vendaEntry.Itens.Sum(i => i.ValorDeVenda - i.Desconto.ValorFixo);
            if (vendaEntry.ValorTotalCalculado <= 0)
            {
                return Support.GetBusinessResultError($"Valor {vendaEntry.ValorTotalCalculado} de venda não permitido", vendaEntry);
            }

            // Adiciona Pontuação
            vendaEntry.Cliente.PontuacaoAdicionalEntrada = vendaEntry.ValorTotalSemCalculo; // A pontuação virá do valor de venda
            var resultadoAdicaoPonto = Service.ExecutarModulo(BusinessModuloCodigo.AdicionaPontuacaoCliente, vendaEntry.Cliente);
            if (resultadoAdicaoPonto.HasError) {
                return Support.GetBusinessResultError("Erro adicionando pontos ao cliente na venda", vendaEntry);
            }

            var repository = Support.GetVendaRepository();
            repository.Salvar(vendaEntry);
            return Support.GetSucessResult(vendaEntry);
        }
    }
}