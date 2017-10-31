using System.Collections.Generic;

namespace GestaoBuilder.Shared.Data.System.Domain
{
    public enum ScriptModuloType
    {
        Indefinido,
        Javascript = 1,
        Python,
        Lua,
    }

    public interface IModulo : IDataSys
    {
        string Nome { get; }
        bool IsObsoleto { get; }
        string Categoria { get; }
        bool IsAgrupamento { get; }
        bool IsPrincipal { get; }
        /// <summary>
        /// Módulo que faz o agrupamento
        /// </summary>
        IModulo Agrupamento { get; }
        /// <summary>
        /// Módulo Anterior
        /// </summary>
        IModulo Anterior { get; }
        IEnumerable<IModuloOrdem> AgrupamentoOrdenacao { get; }
    }

    public interface IScriptModulo : IModulo
    {
        ScriptModuloType ScriptTipo { get; }

        int ScriptResourceId { get; }

        string ScriptMethod { get; }

        string ScriptResourceText { get; }
    }

    public interface IAssemblyModulo : IModulo
    {
        string Assembly { get; }

        string AssemblyFullPath { get; }
    }

    public interface IModuloOrdem : IDataSys
    {
        IModulo Agrupamento { get; }
        IModulo Executor { get; }
        int Ordem { get; }
        bool IsPrincipal { get; }
    }
}