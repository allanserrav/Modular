using System;
using System.Collections.Generic;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.System.Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GestaoBuilder.CoreBusiness.Exemplos
{
    public class FibonacciEntry
    {
        public int Len { get; set; }

        public static IContractResolver GetContractResolver(ISupport support)
        {
            var mapper = DataMapper<FibonacciEntry>.Inialize("")
                    .PropertyParseAndWrite(e => e.Len, "len")
                ;
            return support.GetContractResolver(mapper);
        }
    }

    [ModuloInfo(Codigo = BusinessModuloCodigo.ExemploCalcularFibonacci, Nome = "Calcular fibonacci")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class CalcularFibonacci : BusinessModuloContext
    {
        private readonly List<int> fibonacciSequence;

        public CalcularFibonacci()
        {
            fibonacciSequence = new List<int>();
        }

        public IResultado ExecuteResult(IEntrada entry, Modulo modulo) {
            Logger.LogInformation("CalcularFibonacci");
            fibonacciSequence.Clear();
            try {
                var fibEntry = entry.Parse<FibonacciEntry>(FibonacciEntry.GetContractResolver(Support)).Result;
                Calcular(0, 1, 1, fibEntry.Len);
                return Support.GetSucessResult(fibonacciSequence);
            }
            catch (Exception ex) {
                return Support.GetExceptionResult("Ocorreu um erro no calculo de fibonacci", ex);
            }
        }

        private void Calcular(int a, int b, int counter, int len)
        {
            if (counter <= len) {
                fibonacciSequence.Add(a);
                Calcular(b, a + b, counter + 1, len);
            }
        }

        public override IResultado Execute(IEntrada entry, IModulo modulo)
        {
            throw new NotImplementedException();
        }
    }
}
