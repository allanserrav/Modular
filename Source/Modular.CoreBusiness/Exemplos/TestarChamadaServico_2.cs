using System;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.System.Domain;
using Microsoft.Extensions.Logging;

namespace GestaoBuilder.CoreBusiness.Exemplos
{
    [ModuloInfo(Codigo = BusinessModuloCodigo.ExemploTestarChamadaServico_2, Nome = "Testar chamada de modulo por serviço 2")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class TestarChamadaServico_2 : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry, IModulo modulo)
        {
            int resultValue2 = entry.Parse<int>().Result;
            resultValue2++;
            Logger.LogDebug($"Vai chamar o módulo {BusinessModuloCodigo.ExemploTestarChamadaServico_3} pelo serviço");
            //var task = service.ExecutarModulo(BusinessModuloCodigo.ExemploTestarChamadaServico_3, new BusinessEntry(JToken.FromObject(resultValue2), entry.UsuarioEntry));
            //var result3 = task.GetAwaiter().GetResult();
            //int resultValue3 = result3.Parse<int>();
            //return BusinessResult.Sucess(resultValue3 + 100);
            throw  new NotImplementedException();
        }

    }
}
