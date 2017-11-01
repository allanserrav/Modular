using System;
using System.Collections.Generic;
using System.Text;

namespace Modular.Shared.Atributos
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuloCategoriaAttribute : Attribute
    {
        public bool IsCategoriaNamespace { get; set; }

        public string Valor { get; set; }
    }
}
