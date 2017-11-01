using System.Linq;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.System.Domain;
using Newtonsoft.Json;

namespace GestaoBuilder.CoreBusiness.Exemplos
{
    [ModuloInfo(Codigo = BusinessModuloCodigo.ExemploCalcularMedia, Nome = "Calcular média")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class CalcularMedia : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry, IModulo modulo)
        {
            var entradas = entry.Parse<int[]>().Result;
            int soma = entradas.Sum();
            return Support.GetSucessResult(soma / entradas.Length);
        }
    }
}
