using System;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.System.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GestaoBuilder.CoreBusiness.Exemplos.AgrupamentoSimples
{
    [ModuloInfo(Codigo = BusinessModuloCodigo.ExemploTestarAgrupamentoSimples, Nome = "Teste agrupamento simples")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class TestarAgrupamentoSimples : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry, IModulo modulo)
        {
            DateTime p1 = entry.Parse<DateTime>(new IsoDateTimeConverter()).Result;
            if(p1.DayOfWeek == DayOfWeek.Monday) {
                return Support.GetSucessResult("É segunda");
            }
            return Support.GetSucessResult("Não é segunda");
        }
    }
}
