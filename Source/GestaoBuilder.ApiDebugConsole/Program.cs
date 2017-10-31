using System;
using System.Threading.Tasks;
using GestaoBuilder.Core;
using GestaoBuilder.CoreShared;
using GestaoBuilder.CoreShared.Contracts;
using GestaoBuilder.CoreShared.Model;
using GestaoBuilder.Data.EFCore;
using GestaoBuilder.Data.MongoCore;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.Business;
using GestaoBuilder.Shared.Data.System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace GestaoBuilder.ApiDebugConsole
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Using sqlserver");
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GestaoBuilder;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            Console.WriteLine($"Sql server: {connectionString}");

            var services = new ServiceCollection();

            services.AddScoped<IModuloServiceCore, ModuloService>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped<IDataRead<IDataSysKey>, EFDataContext>();
            //services.AddScoped<IScriptResource, FileScriptResource>();
            services.AddScoped<CoreSupportService>();
            services.AddScoped<ModuloReader>();
            services.AddScoped<IScriptResource, MockScriptResource>();
            services.AddScoped((Func<IServiceProvider, ICoreSupport>)(p => {
                var supportService = p.GetService<CoreSupportService>();
                return supportService;
            }));
            services.AddScoped<IDataBisRead, MongoReader>();
            services.AddScoped<IDataBisWrite, MongoWriter>();

            services.AddSingleton(new LoggerFactory()
                .AddConsole()
                //.AddDebug()
                );
            services.AddLogging();

            services.AddDbContext<EFDataContext>(options =>
                options.UseSqlServer(connectionString)
            );

            //services.AddOptions();

            await Initialize(services.BuildServiceProvider());
        }

        private static async Task Initialize(ServiceProvider provider)
        {
            var loggerFactory = provider.GetService<ILoggerFactory>();
            //loggerFactory.AddSeq(minimumLevel: LogLevel.Debug);
            //loggerFactory.AddConsole();
            //loggerFactory.AddDebug(LogLevel.Debug);

            CoreSupportService core = provider.GetService<CoreSupportService>();
            core.UsuarioLogado = new Usuario {
                Codigo = "S0001",
                Nome = "Usuario teste",
                SegundosLoginExpirar = 1000,
                EmpresaEmUSo = new Empresa {
                    ConnectionStringDb = "mongodb://localhost:27017",
                    DatabaseName = "GestaoSocial"
                }
            };
            core.UsuarioEntry = core.UsuarioLogado;

            //await TesteModuloControllerProcessarSalvaCliente(provider);
            //await TesteModuloControllerProcessarListaCliente(provider);
            //await TesteModuloControllerProcessarAdicionaPontuacaoCliente(provider);
            await TesteModuloControllerProcessarRealizaVenda(provider);
        }

        private static async Task TesteModuloControllerProcessarSalvaCliente(ServiceProvider provider)
        {
            var core = provider.GetService<CoreSupportService>();
            var service = provider.GetService<IModuloServiceCore>();

            // Salvar cliente
            string modulo = "AGR103";
            string json = @"{
                ""codigo"":""CLI001"",
                ""nome"":""Allan Serra Vasconcellos"",
                ""apelido"":""Gigante"",
                ""nascimento"":""1981-08-04"",
                ""email"" : ""vasconcellos.ti@hotmail.com"",
                ""telefone"" : ""(51) 99951-2584"",
                ""pontuacao_limite"" : 20,
                ""pontuacao_atual"" : 5,
                ""creditar_limite_pontuacao"" : 50,
                ""anotacao"" : ""Bonitão""
            }";
            var result = await service.ExecutarModuloCore(modulo, new Entrada(JToken.Parse(json), core.UsuarioEntry, TipoOperacaoEnum.Novo));
            if (!result.IsException) {

            }
        }

        private static async Task TesteModuloControllerProcessarAdicionaPontuacaoCliente(ServiceProvider provider)
        {
            var core = provider.GetService<CoreSupportService>();
            var service = provider.GetService<IModuloServiceCore>();

            // Adiciona pontuação
            string modulo = "CLI003";
            string json = @"{
                ""codigo_cliente"":""CLI001"",
                ""pontuacao"":60
            }";
            var result = await service.ExecutarModuloCore(modulo, new Entrada(JToken.Parse(json), core.UsuarioEntry, TipoOperacaoEnum.Novo));
            if (!result.IsException) {

            }
        }

        private static async Task TesteModuloControllerProcessarListaCliente(ServiceProvider provider)
        {
            var core = provider.GetService<CoreSupportService>();
            var service = provider.GetService<IModuloServiceCore>();

            // Listar clientes
            string modulo = "CLIL004";
            string json = @"{
                ""codigo"":""CLI001""
            }";
            var result = await service.ExecutarModuloCore(modulo, new Entrada(JToken.Parse(json), core.UsuarioEntry, TipoOperacaoEnum.Novo));
            if (!result.IsException) {

            }
        }

        private static async Task TesteModuloControllerProcessarRealizaVenda(ServiceProvider provider)
        {
            var core = provider.GetService<CoreSupportService>();
            var service = provider.GetService<IModuloServiceCore>();

            // Listar clientes
            string modulo = "AGR104";
            string json = @"{
                ""anotacao"":""Teste anotação"",
                ""cliente"":{ ""codigo"" : ""CLI001"" },
                ""itens"":
                [
                    {
                        ""produto"" : { ""codigo"" : ""HP001"" },
                        ""desconto"" : { ""valor_fixo"" : 5 }
                    }
                ]
            }";
            var result = await service.ExecutarModuloCore(modulo, new Entrada(JToken.Parse(json), core.UsuarioEntry, TipoOperacaoEnum.Novo));
            if (!result.HasError)
            {
                var jToken = result.ResultJson();
            }
        }
    }
}
