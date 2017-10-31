using System;
using System.Text;
using GestaoBuilder.CoreShared.Model;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.Business;
using GestaoBuilder.Shared.Data.System.Domain;
using Microsoft.Extensions.Logging;
using VroomJs;

namespace GestaoBuilder.Core.Javascript
{
    public class JavascriptEngine : IModuloContext
    {
        public JavascriptEngine(ScriptModulo modulo, IScriptResource resource)
        {
            Modulo = modulo;
            Resource = resource;
        }

        public ScriptModulo Modulo { get; }
        public IScriptResource Resource { get; }

        private ILogger logger;
        private IDataBisRead reader;
        private IModuloService service;
        private IElapsedTimeWatch watch;
        private IDataBisWrite writer;

        public void AddLogger(ILogger logger)
        {
            this.logger = logger;
        }

        public void AddReader(IDataBisRead reader)
        {
            this.reader = reader;
        }

        public void AddService(IModuloService service)
        {
            this.service = service;
        }

        public void AddMappers(IMapper mapper)
        {
            throw new NotImplementedException();
        }

        public void AddElapsedTime(IElapsedTimeWatch time)
        {
            this.watch = time;
        }

        public void AddSupport(ISupport support)
        {
            throw new NotImplementedException();
        }

        public void AddModuto(IModulo modulo)
        {
            throw new NotImplementedException();
        }

        public void AddWriter(IDataBisWrite writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Inicializa os serviços para o runtime v8
        /// </summary>
        /// <exception cref="msvcr110.dll">
        /// O erro "msvcr110.dll is missing" necessita que o Microsoft Visual C++ Redistributable 2012 seja instalado quando o mesmo não está 
        /// http://www.standaloneofflineinstallers.com/2015/12/Microsoft-Visual-C-Redistributable-2015-2013-2012-2010-2008-2005-32-bit-x86-64-bit-x64-Standalone-Offline-Installer-for-Windows.html
        /// </exception>
        public static void Initialize()
        {
            AssemblyLoader.EnsureLoaded();
        }

        public IResultado Execute(IEntrada entry)
        {
            string scriptCode = Resource.GetJavascriptResource(Modulo);
            using (JsEngine js = new JsEngine(4, 32)) {
                using (JsContext context = js.CreateContext()) {
                    //context.SetVariable("entry", entry.EntryDyn);
                    if (!String.IsNullOrEmpty(Modulo.ScriptMethod)) {
                        StringBuilder builder = new StringBuilder(scriptCode);
                        builder.AppendLine();
                        builder.AppendLine($"{Modulo.ScriptMethod}();");
                        scriptCode = builder.ToString();
                    }
                    context.Execute(scriptCode);
                    dynamic x = context.GetVariable("x");
                }
            }
            throw new NotImplementedException();
        }
    }
}
