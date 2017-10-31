using System;
using System.IO;
using GestaoBuilder.Core.Javascript;
using GestaoBuilder.CoreShared.Model;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.System.Domain;
using GestaoBuilder.Shared.Helpers;

namespace GestaoBuilder.Core
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
