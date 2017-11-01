using Modular.BackgroundService.Services;
using Modular.Shared.Contracts;
using Modular.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Modular.Shared;
using System.Collections.Generic;
using System.Linq;
using Modular.CoreShared.Model;
using Modular.Shared.Data;
using Modular.Shared.Data.System;
using Microsoft.Extensions.Options;

namespace Modular.BackgroundService
{
    class Program
    {
        private static IServiceProvider provider;

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            Console.WriteLine("Using sqlserver");
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine($"Sql server: {connectionString}");

            var service = new ServiceCollection()
                .AddDbContext<EFDataContext>(options =>
                    options.UseSqlServer(connectionString));

            service.AddScoped<IUnitOfWork, EFUnitOfWork>();
            service.AddScoped<IDataRead<IDataSysKey>, EFDataContext>();

            service.AddOptions();
            service.Configure<BackgroundConfiguration>(t => t.CarregarConfiguration(configuration));

            provider = service.BuildServiceProvider();
            Initialize();
        }

        private static void Initialize()
        {
            var configuration = provider.GetService<IOptions<BackgroundConfiguration>>();
            var unit = provider.GetService<IUnitOfWork>();
            var reader = provider.GetService<IDataRead<IDataSysKey>>();
            var assemblyService = new AtualizacaoAssemblyModulo(configuration, unit, reader);
            assemblyService.OnProcessed += AtualizacaoAssemblyModuloOnProcessed;
            assemblyService.Atualizar();
        }

        private static void AtualizacaoAssemblyModuloOnProcessed(object sender, AtualizacaoAssemblyModuloEventArgs e)
        {
#if DEBUG
            var unit = provider.GetService<IUnitOfWork>();
            var reader = unit.ReadContext;

            // Agrupamento para configuracao de mappers
            var moduloConfigura = e.Todos.FirstOrDefault(d => d.Codigo.Equals("CFM001"));
            var modulo = reader.GetByCodigo<Modulo>("AGR001");
            if (modulo == null && moduloConfigura != null) {
                using (var tr = unit.BeginTransaction()) {
                    var writer = unit.WriteContext;
                    var executores = new List<ModuloOrdem>();
                    var agrupamento = new Modulo() {
                        Codigo = "AGR001",
                        Categoria = "",
                        CreatedOn = DateTime.Now,
                        Nome = "Agrupamento mappers",
                        IsAgrupamento = true,
                    };
                    executores.Add(new ModuloOrdem() {
                        ModuloAgrupamento = agrupamento,
                        CreatedOn = DateTime.Now,
                        Ordem = 1,
                        IsPrincipal = true,
                        ModuloExecutor = moduloConfigura
                    });
                    agrupamento.AgrupamentoOrdenacaoIn = executores;
                    writer.Add(agrupamento);
                    tr.Commit();
                }
            }
#endif
            Console.WriteLine($"Arquivo {e.FileName}");
            Console.WriteLine($"{e.Adicionados.Count()} itens foram adicionados");
            Console.WriteLine($"{e.Atualizados.Count()} itens foram atualizados");
        }
    }
}
