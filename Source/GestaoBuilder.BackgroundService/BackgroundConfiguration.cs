using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Bases;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GestaoBuilder.BackgroundService
{
    public class BackgroundConfiguration
    {
        private IConfigurationRoot configuration;
        private string contentRootPath;
        private string[] businessAssemblies;

        public void CarregarConfiguration(IConfigurationRoot configuration)
        {
            this.configuration = configuration;
            CarregarGeneralInfo();
            CarregarAssemblies();
        }

        private void CarregarAssemblies()
        {
            var section = configuration.GetSection("Assemblies");
            List<string> assemblies = new List<string>();
            foreach (var a in section.AsEnumerable()) {
                if (!String.IsNullOrEmpty(a.Value)) {
                    assemblies.Add(a.Value);
                }
            }
            businessAssemblies = assemblies.ToArray();
        }

        private void CarregarGeneralInfo()
        {
            var section = configuration.GetSection("GeneralInfo");
            contentRootPath = section["ContentRootPath"];
        }

        public string ScriptPath => Path.Combine(contentRootPath, "Scripts");
        public string AssembliesPath => contentRootPath;
        public string[] BusinessAssemblies => businessAssemblies;
    }
}
