using Modular.ClienteBusinessTeste.Domain;
using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.System.Domain;
using Newtonsoft.Json;

namespace Modular.ClienteBusinessTeste.Mappers
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