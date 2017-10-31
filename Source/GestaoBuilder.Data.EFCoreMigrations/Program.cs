using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Data.EFCore;
using GestaoBuilder.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.System;

namespace GestaoBuilder.Data.EFCoreMigrations
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            Console.WriteLine("Using sqlserver");
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            string dataAssemblyname = "GestaoBuilder.Data.EFCoreMigrations";
            Console.WriteLine($"Sql server: {connectionString}");
            Console.WriteLine($"Migration Assembly: {dataAssemblyname}");

            var service = new ServiceCollection()
                .AddDbContext<EFDataContext>(options =>
                    options.UseSqlServer(connectionString));
                ;

            service.AddScoped<IUnitOfWork, EFUnitOfWork>();
            service.AddScoped<IDataRead<IDataSysKey>, EFDataContext>();

            //InitializeDataModuloScript(configuration, service.BuildServiceProvider());
            //InitializeDataModuloAgrupamento(configuration, service.BuildServiceProvider());
            //TesteEmpresa(service.BuildServiceProvider());
        }
    }
}
