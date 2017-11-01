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
    [ModuloInfo(Codigo = "ABC101", Nome = "Adiciona mapeamento para o produto ajustado")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class AdicionaProdutoAjustadoMappers : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry, IModulo modulo)
        {
            var parseResult = entry.Parse<IDataMapperManager>(false);
            var manager = parseResult.IsParseSucess ? parseResult.Result : Support.GetDataMapperManager();
            manager.AddInheritance<ProdutoAjustado, Produto>(d =>
            {
                d.PropertyParseAndWrite(e => e.ParamAjuste, "param_ajuste");
                d.PropertyParseAndWrite(e => e.Ajuste, "ajuste");
            });
            return Support.GetSucessResult(manager);
        }
    }
}