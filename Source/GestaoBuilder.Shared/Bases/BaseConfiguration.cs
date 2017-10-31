using GestaoBuilder.Shared.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GestaoBuilder.Shared.Bases
{
    public abstract class BaseConfiguration : IConfigurationSystem
    {
        private readonly string contentRootPath;

        public BaseConfiguration(string contentRootPath)
        {
            this.contentRootPath = contentRootPath;
        }

        public abstract string[] BusinessAssemblies {
            get;
        }

        public virtual string ScriptPath { get { return Path.Combine(contentRootPath, "Scripts"); } }
    }
}
