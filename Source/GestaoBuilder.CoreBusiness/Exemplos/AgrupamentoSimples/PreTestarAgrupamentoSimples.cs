using System;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.System.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GestaoBuilder.CoreBusiness.Exemplos.AgrupamentoSimples
{
    [ModuloInfo(Codigo = BusinessModuloCodigo.ExemploPreTestarAgrupamentoSimples, Nome = "Pre teste agrupamento simples")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class PreTestarAgrupamentoSimples : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry, IModulo modulo)
        {
            DateTime p1 = entry.Parse<DateTime>(new IsoDateTimeConverter()).Result;
            p1 = p1.AddDays(3);
            return Support.GetSucessResult(p1);
        }
    }
}
