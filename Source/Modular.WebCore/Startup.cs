using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modular.CoreShared.Contracts;
using Modular.Core;
using Modular.Data.EFCore;
using Modular.Shared.Data;
using Modular.Shared.Data.System;
using Modular.Shared.Contracts;
using Microsoft.Extensions.Logging;
using Modular.Data.MongoCore;
using Modular.Shared.Data.Business;
using Microsoft.EntityFrameworkCore;
using Modular.Core.Contracts;

namespace GestaoBuilder_WebCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.AddMemoryCache();

            services.AddScoped<IModuloServiceCore, ModuloService>();
            services.AddScoped<ICoreSupportService, WebCoreSupport>();
            services.AddScoped<ICoreSupport>(p =>
            {
                var supportService = p.GetService<ICoreSupportService>();
                return supportService;
            });
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            services.AddScoped<IDataRead<IDataSysKey>, EFDataContext>();
            services.AddScoped<IScriptResource, FileScriptResource>();
            services.AddScoped<ModuloReader>();

            services.AddScoped<IDataBisRead, MongoReader>();
            services.AddScoped<IDataBisWrite, MongoWriter>();

            services.AddSingleton<IConfigurationSystem, WebCoreConfigurationSystem>(p =>
            {
                var env = p.GetService<IHostingEnvironment>();
                return new WebCoreConfigurationSystem(env);
            });

            services.AddDbContext<EFDataContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
