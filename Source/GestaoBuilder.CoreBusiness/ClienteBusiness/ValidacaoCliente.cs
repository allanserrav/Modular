using System;
using System.Linq;
using GestaoBuilder.CoreBusiness.ClienteBusiness.Validators;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.Business.Domain;
using GestaoBuilder.Shared.Data.System.Domain;

namespace GestaoBuilder.CoreBusiness.ClienteBusiness
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