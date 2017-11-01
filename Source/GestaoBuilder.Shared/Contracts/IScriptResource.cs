using Modular.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using Modular.Shared.Data.System.Domain;

namespace Modular.Shared.Contracts
{
    public interface IScriptResource
    {
        string GetJavascriptResource(IScriptModulo modulo);
    }
}
