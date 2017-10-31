using GestaoBuilder.Shared.Bases;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoBuilder_WebCore
{
    public class WebCoreConfigurationSystem : BaseConfiguration
    {
        private readonly IHostingEnvironment env;

        public WebCoreConfigurationSystem(IHostingEnvironment env)
            : base(env.ContentRootPath)
        {
            this.env = env;
        }

        public override string[] BusinessAssemblies => Array.Empty<string>();
    }
}
