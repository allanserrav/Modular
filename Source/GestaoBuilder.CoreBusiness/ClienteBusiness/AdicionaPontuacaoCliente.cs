using System.Linq;
using Modular.CoreBusiness.ClienteBusiness.Entries;
using Modular.CoreBusiness.ClienteBusiness.Validators;
using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.System.Domain;

namespace Modular.CoreBusiness.ClienteBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.AdicionaPontuacaoCliente, Nome = "Adiciona pontuação ao cliente")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class AdicionaPontuacaoCliente : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            var clienteEntry = entry.Parse<Cliente>(Mapper).Result;
            var validator = new ClienteEntryValidator();
            //var validaResult = validator.Validate(pontuacaoEntry);
            //if (!validaResult.IsValid)
            //{
            //    var messages = validaResult.Errors.Select(c=> new ResultadoMessage { ChaveMessage = c.ErrorCode, Message = c.ErrorMessage, TipoMessage = TipoMessageEnum.Validacao});
            //    return Support.GetValidacaoResultError(messages, pontuacaoEntry);
            //}
            var repository = Support.GetClienteRepository();
            var clientePonto = new ClientePonto { Pontos = clienteEntry.PontuacaoAdicionalEntrada };
            clienteEntry.PontuacaoAtual = clienteEntry.PontuacaoAtual + clienteEntry.PontuacaoAdicionalEntrada;
            if (clienteEntry.PontuacaoAtual >= clienteEntry.PontuacaoLimite) {
                var clienteCredito = new ClienteCredito { Valor = clienteEntry.CreditarLimitePontuacao };
                clienteEntry.AddCredito(clienteCredito);
                clientePonto.GerouCredito = true;
                clienteEntry.PontuacaoAtual = 0;
            }
            clienteEntry.AddHistoricoPontos(clientePonto);
            repository.Salvar(clienteEntry);
            return Support.GetSucessResult(true);
        }
    }
}