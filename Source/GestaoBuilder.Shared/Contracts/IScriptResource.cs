using GestaoBuilder.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using GestaoBuilder.Shared.Data.System.Domain;

namespace GestaoBuilder.Shared.Contracts
{
    public interface IScriptResource
    {
        string GetJavascriptResource(IScriptModulo modulo);
    }
}
