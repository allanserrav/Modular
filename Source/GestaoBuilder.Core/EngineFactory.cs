using System;
using System.IO;
using Modular.Core.Javascript;
using Modular.CoreShared.Model;
using Modular.Shared;
using Modular.Shared.Contracts;
using Modular.Shared.Data.System.Domain;
using Modular.Shared.Helpers;

namespace Modular.Core
{
    public static class EngineFactory
    {
        public static IModuloContext GetInstance(Modulo modulo, IScriptResource resource)
        {
            if (modulo is AssemblyModulo) {
                var assemblyModulo = modulo as AssemblyModulo;
                //http://www.red-gate.com/products/dotnet-development/reflector/
                var file = new FileInfo(assemblyModulo.AssemblyFullPath);
                return AssemblyHelper.GetObjectFromFile<IModuloContext>(file, assemblyModulo.Assembly);
                //return new AssemblyEngine(modulo as AssemblyModulo);
            }
            else if (modulo is ScriptModulo) {
                var scriptModulo = modulo as ScriptModulo;
                switch (scriptModulo.ScriptTipo) {
                    case ScriptModuloType.Javascript:
                        return new JavascriptEngine(scriptModulo, resource);
                    default:
                        throw new NotImplementedException();
                }
            }

            throw new NotImplementedException();
        }
    }
}
