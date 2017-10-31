using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared;
using System.IO;
using GestaoBuilder.CoreShared.Model;
using GestaoBuilder.Shared.Data.System.Domain;
using GestaoBuilder.Shared.Helpers;

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
