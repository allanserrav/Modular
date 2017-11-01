using System.Collections.Generic;
using Modular.CoreShared.Bases;
using Modular.Shared;
using Modular.Shared.Data.System.Domain;

namespace Modular.CoreShared.Model
{
    public class Modulo : BaseEntidade, IModulo
    {
        public string Nome { get; set; }
        public bool IsObsoleto { get; set; }
        public string Categoria { get; set; }
        public bool IsAgrupamento { get; set; }
        public bool IsPrincipal { get; set; }
        /// <summary>
        /// Módulo que faz o agrupamento
        /// </summary>
        public Modulo AgrupamentoIn { get; set; }
        /// <summary>
        /// Módulo Anterior
        /// </summary>
        public Modulo AnteriorIn { get; set; }
        public IEnumerable<ModuloOrdem> AgrupamentoOrdenacaoIn { get; set; }

        IModulo IModulo.Agrupamento => AgrupamentoIn;

        IModulo IModulo.Anterior => AnteriorIn;

        IEnumerable<IModuloOrdem> IModulo.AgrupamentoOrdenacao => AgrupamentoOrdenacaoIn;
    }

    public class ModuloOrdem : BaseEntidade, IModuloOrdem
    {
        public Modulo ModuloAgrupamento { get; set; }
        public Modulo ModuloExecutor { get; set; }
        public int Ordem { get; set; }
        public bool IsPrincipal { get; set; }

        IModulo IModuloOrdem.Agrupamento => ModuloAgrupamento;

        IModulo IModuloOrdem.Executor => ModuloExecutor;
    }

    public class ScriptModulo : Modulo, IScriptModulo
    {
        public ScriptModuloType ScriptTipo { get; set; }

        public int ScriptResourceId { get; set; }

        public string ScriptMethod { get; set; }

        public string ScriptResourceText { get; set; }
    }

    public class AssemblyModulo : Modulo, IAssemblyModulo
    {
        public string Assembly { get; set; }

        public string AssemblyFullPath { get; set; }
    }
}
