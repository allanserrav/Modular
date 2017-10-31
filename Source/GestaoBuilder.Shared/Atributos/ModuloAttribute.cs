using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoBuilder.Shared.Atributos
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuloAttribute : Attribute
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string AgrupamentoCodigo { get; set; }
        public int AgrupamentoOrdem { get; set; }
        public bool PrincipalNoAgrupamento { get; set; }
    }
}
