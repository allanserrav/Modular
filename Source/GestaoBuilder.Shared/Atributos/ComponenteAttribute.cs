using System;

namespace Modular.Shared.Atributos
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ComponenteAttribute : Attribute
    {
        public string Codigo { get; set; }
        public string CodigoGrupo { get; set; }
    }
}