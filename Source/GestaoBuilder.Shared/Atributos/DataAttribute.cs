using System;

namespace Modular.Shared.Atributos
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DataAttribute : Attribute
    {
        public string DocumentName { get; set; }
    }
}
