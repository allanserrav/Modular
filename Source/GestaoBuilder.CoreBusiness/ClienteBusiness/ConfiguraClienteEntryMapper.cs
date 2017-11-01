using Modular.CoreBusiness.ClienteBusiness.Entries;
using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data;
using Modular.Shared.Data.System.Domain;

namespace Modular.CoreBusiness.ClienteBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.ConfiguraClienteEntryMapper, Nome = "Configuração de mapeamento de dados do cliente entry", 
                AgrupamentoCodigo = BusinessModuloCodigo.AgrupamentoMapeamento, AgrupamentoOrdem = 2)]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class ConfiguraClienteEntryMapper : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            var mapper = entry.Parse<IMapperManager>().Result;
            mapper
                .AddMapParse<ClienteEntry>(d => {
                    d.PropertyParse(p => p.ClienteCodigo, "codigo_cliente");
                    d.PropertyParse(p => p.Pontuacao, "pontuacao");
                });
            return Support.GetSucessResult(mapper);
        }
    }
}
