using System;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.System.Domain;

namespace GestaoBuilder.ApiDebugConsole
{
    public class MockScriptResource : IScriptResource
    {
        public string GetJavascriptResource(IScriptModulo modulo)
        {
            return String.Empty;
        }
    }
}