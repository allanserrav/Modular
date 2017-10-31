using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text;
using GestaoBuilder.Data.EFCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GestaoBuilder.Data.EFCoreMigrations
{
    public class Startup
    {
        //public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("Using sqlserver");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();
            //string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=GestaoBuilder;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            string dataAssemblyname = "GestaoBuilder.Data.EFCoreMigrations";
            Console.WriteLine($"Sql server: {connectionString}");
            Console.WriteLine($"Migration Assembly: {dataAssemblyname}");
            services.AddDbContext<EFDataContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(dataAssemblyname))
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(ILoggerFactory loggerFactory)
        {
        }
    }
}
