using System;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.System.Domain;
using Microsoft.Extensions.Logging;

namespace GestaoBuilder.CoreBusiness.Exemplos
{
    [ModuloInfo(Codigo = BusinessModuloCodigo.ExemploTestarChamadaServico_3, Nome = "Testar chamada de modulo por serviço 3")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class TestarChamadaServico_3 : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry, IModulo modulo)
        {
            //using (Logger.BeginScope($"=>{ nameof(DoExecute) }")) {
            int resultValue3 = entry.Parse<int>().Result;
            resultValue3++;
            ElapsedTime.WaitFor(TimeSpan.FromSeconds(1));
            Logger.LogDebug($"Vai chamar o módulo {BusinessModuloCodigo.ExemploTestarChamadaServico_4} pelo serviço");
            //var task = service.ExecutarModulo(BusinessModuloCodigo.ExemploTestarChamadaServico_4, new BusinessEntry(JToken.FromObject(resultValue3), entry.UsuarioEntry));
            //var result4 = task.GetAwaiter().GetResult();
            //int resultValue4 = result4.Parse<int>();
            //return BusinessResult.Sucess(resultValue4 + 100);
            //}
            throw new NotImplementedException();
        }
    }
}
