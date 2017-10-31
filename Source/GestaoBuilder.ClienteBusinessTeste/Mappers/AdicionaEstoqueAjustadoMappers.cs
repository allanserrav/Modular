using GestaoBuilder.ClienteBusinessTeste.Domain;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.Business.Domain;
using GestaoBuilder.Shared.Data.System.Domain;
using Newtonsoft.Json;

namespace GestaoBuilder.ClienteBusinessTeste.Mappers
{
    [ModuloInfo(Codigo = "ABC002", Nome = "Adiciona mapeamento para o estoque ajustado")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class AdicionaEstoqueAjustadoMappers : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry, IModulo modulo)
        {
            var parseResult = entry.Parse<IDataMapperManager>();
            var manager = parseResult.IsParseSucess ? parseResult.Result : Support.GetDataMapperManager();
            manager.AddInheritance<EstoqueAjustado, Estoque>(d =>
            {
                d.PropertyParseAndWrite(e => e.Ajuste, "ajuste");
            });
            return Support.GetSucessResult(manager);
        }
    }
}