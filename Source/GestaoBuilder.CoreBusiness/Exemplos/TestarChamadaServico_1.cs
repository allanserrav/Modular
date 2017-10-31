using System;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.System.Domain;
using Microsoft.Extensions.Logging;

namespace GestaoBuilder.CoreBusiness.Exemplos
{
    [ModuloInfo(Codigo = BusinessModuloCodigo.ExemploTestarChamadaServico_1, Nome = "Testar chamada de modulo por serviço 1")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class TestarChamadaServico_1 : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry, IModulo modulo)
        {
            Logger.LogDebug("Iniciando TestarChamadaServico_1");
            int resultValue1 = entry.Parse<int>().Result;
            Logger.LogDebug($"Vai chamar o módulo {BusinessModuloCodigo.ExemploTestarChamadaServico_2} pelo serviço");
            //var task = service.ExecutarModulo(BusinessModuloCodigo.ExemploTestarChamadaServico_2, new BusinessEntry(JToken.FromObject(resultValue1), entry.UsuarioEntry));
            //var result2 = task.GetAwaiter().GetResult();
            //int resultValue2 = result2.Parse<int>();
            //return BusinessResult.Sucess(resultValue2 + 100);
            throw new NotImplementedException();
        }
    }
}
