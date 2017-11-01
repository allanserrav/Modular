using System;
using System.Linq;
using Modular.CoreBusiness.ClienteBusiness.Validators;
using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.System.Domain;

namespace Modular.CoreBusiness.ClienteBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.ValidacaoCliente, Nome = "Valida dados do cliente",
                AgrupamentoCodigo = BusinessModuloCodigo.AgrupamentoSalvaCliente, AgrupamentoOrdem = 1)]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class ValidacaoCliente : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            var cliente = entry.Parse<Cliente>(Mapper).Result;
            var validator = new ClienteValidator();
            var result = validator.Validate(cliente);
            if (!result.IsValid) {
                return Support.GetValidacaoResultError(result.Errors.GetValidacaoMessage(), cliente);
            }
            return Support.GetSucessResult(cliente);
        }
    }
}