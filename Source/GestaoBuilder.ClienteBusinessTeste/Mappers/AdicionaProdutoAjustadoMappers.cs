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