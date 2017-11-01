using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.System.Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GestaoBuilder.CoreBusiness.Exemplos
{
    [ModuloInfo(Codigo = BusinessModuloCodigo.ExemploTestarChamadaServico_5, Nome = "Testar chamada de modulo por serviço 5")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class TestarChamadaServico_5 : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry, IModulo modulo)
        {
            //using (Logger.BeginScope($"=>{ nameof(DoExecute) }")) {
            Logger.LogDebug("Iniciando TestarChamadaServico_3");
            int result3 = entry.Parse<int>().Result;
            result3 = result3 * 100;
            return Support.GetSucessResult(result3);
            //}
        }
    }
}
