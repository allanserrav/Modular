using Modular.Shared.Contracts;
using Modular.Shared;
using System.IO;
using Modular.CoreShared.Model;
using Modular.Shared.Data.System.Domain;
using Modular.Shared.Helpers;

namespace GestaoBuilder_WebCore
{
    public class FileScriptResource : IScriptResource
    {
        private readonly IConfigurationSystem configuration;

        public FileScriptResource(IConfigurationSystem configuration)
        {
            this.configuration = configuration;
        }

        public string GetJavascriptResource(IScriptModulo modulo)
        {
            string filePath = Path.Combine(configuration.ScriptPath, modulo.ScriptResourceId.ToString("0000") + ".js");
            return FileHelper.GetTextFromFilePath(filePath);
        }
    }
}
