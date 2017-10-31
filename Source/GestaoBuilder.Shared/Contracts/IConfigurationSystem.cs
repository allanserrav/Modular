using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoBuilder.Shared.Contracts
{
    public interface IConfigurationSystem
    {
        string ScriptPath { get; }

        string[] BusinessAssemblies { get; }
    }
}
