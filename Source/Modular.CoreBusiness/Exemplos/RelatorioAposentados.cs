using System;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.System.Domain;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace GestaoBuilder.CoreBusiness.Exemplos
{
    [ModuloInfo(Codigo = BusinessModuloCodigo.ExemploRelatorioAposentados, Nome = "Relatório de aposentados por periodo")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class RelatorioAposentados : BusinessModuloContext
    {
        public class Aposentado
        {
            public string Codigo { get; private set; }
            public string Nome { get; set; }
            public int Idade { get; private set; }
        }

        public class RelatorioEntry
        {
            public DateTime Inicio { get; set; }

            public DateTime Fim { get; set; }

            public static IContractResolver GetContractResolver(ISupport support)
            {
                var mapper = DataMapper<RelatorioEntry>.Inialize("")
                        .PropertyParseAndWrite(e => e.Inicio, "inicio")
                        .PropertyParseAndWrite(e => e.Fim, "fim")
                    ;
                return support.GetContractResolver(mapper);
            }
        }

        public override IResultado Execute(IEntrada entry, IModulo modulo)
        {
            var relEntry = entry.Parse<RelatorioEntry>(RelatorioEntry.GetContractResolver(Support), new IsoDateTimeConverter());

            throw new NotImplementedException();
        }
    }
}
