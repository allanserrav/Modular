using System;
using Modular.Shared.Contracts;
using Modular.Shared.Data.System.Domain;

namespace Modular.ApiDebugConsole
{
    public class MockScriptResource : IScriptResource
    {
        public string GetJavascriptResource(IScriptModulo modulo)
        {
            return String.Empty;
        }
    }
}