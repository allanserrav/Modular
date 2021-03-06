﻿using System;
using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.System.Domain;

namespace Modular.CoreBusiness.ClienteBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.SalvaCliente, Nome = "Salvar dados do cliente",
                AgrupamentoCodigo = BusinessModuloCodigo.AgrupamentoSalvaCliente, AgrupamentoOrdem = 2, PrincipalNoAgrupamento = true)]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class SalvaCliente : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            if (entry.ResultadoAnterior.IsValidacaoError) {
                return entry.ResultadoAnterior;
            }
            var cliente = entry.Parse<Cliente>(Mapper).Result;
            var repository = Support.GetClienteRepository();
            cliente.PontuacaoAtual = 0;
            cliente.PontuacaoLimite = 100;
            cliente.CreditarLimitePontuacao = 25;
            repository.Salvar(cliente);
            return Support.GetSucessResult(cliente);
        }
    }
}