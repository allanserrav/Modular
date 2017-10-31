using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GestaoBuilder.CoreShared.Model;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.System;
using GestaoBuilder.Shared.Helpers;
using Microsoft.Extensions.Options;

namespace GestaoBuilder.BackgroundService.Services
{
    public class AtualizacaoAssemblyModuloEventArgs : EventArgs
    {
        public string FileName { get; }
        private readonly List<AssemblyModulo> adicionados;
        public IEnumerable<AssemblyModulo> Adicionados => adicionados;

        private readonly List<AssemblyModulo> atualizados;
        public IEnumerable<AssemblyModulo> Atualizados => atualizados;

        private readonly List<AssemblyModulo> todos;
        public IEnumerable<AssemblyModulo> Todos => todos;

        public AtualizacaoAssemblyModuloEventArgs(string fileName, IEnumerable<AssemblyModulo> adicionados, IEnumerable<AssemblyModulo> atualizados)
        {
            FileName = fileName;
            this.adicionados = new List<AssemblyModulo>(adicionados);
            this.atualizados = new List<AssemblyModulo>(atualizados);
            this.todos = new List<AssemblyModulo>();
            this.todos.AddRange(adicionados);
            this.todos.AddRange(atualizados);
        }
    }

    public class AtualizacaoAssemblyModulo
    {
        private readonly BackgroundConfiguration configuration;
        private readonly IUnitOfWork unit;
        private readonly IDataRead<IDataSysKey> reader;

        public event EventHandler<AtualizacaoAssemblyModuloEventArgs> OnProcessed;

        public AtualizacaoAssemblyModulo(IOptions<BackgroundConfiguration> configuration, IUnitOfWork unit, IDataRead<IDataSysKey> reader)
        {
            this.configuration = configuration.Value;
            this.unit = unit;
            this.reader = reader;
        }

        public void Atualizar()
        {
            var source = new TaskCompletionSource<string>();
            var watcher = new FileWatcher(configuration.AssembliesPath, source);
            watcher.OnChange += Watcher_On;
            watcher.OnAdd += Watcher_On;
            foreach (var a in configuration.BusinessAssemblies) {
                watcher.AddWatch(a);
            }
            watcher.Wait(true);
        }

        private void Watcher_On(object sender, FileWatcherEventArgs e)
        {
            try {
                var file = e.FileChange;
                var adicionados = new List<AssemblyModulo>();
                var atualizados = new List<AssemblyModulo>();
                foreach (var moduloType in AssemblyHelper.GetInterfaceTypes<IModuloContext>(file.FullName)) {
                    var attrs = moduloType.GetAtributos();
                    var attributes = attrs as Attribute[] ?? attrs.ToArray();
                    var info = attributes.FirstOrDefault(a => a is ModuloAttribute) as ModuloAttribute;
                    if (info == null) continue; // Não tem informação para o modulo, assim não faz nada
                    var modulo = reader.GetByCodigo<Modulo>(info.Codigo);
                    using (var tr = unit.BeginTransaction()) {
                        var categoria =
                            attributes.FirstOrDefault(a => a is ModuloCategoriaAttribute) as ModuloCategoriaAttribute;
                        if (modulo is AssemblyModulo assemblyModulo) {
                            // atualizar o modulo
                            assemblyModulo.UpdatedOn = DateTime.Now;
                            assemblyModulo.IsObsoleto =
                                attributes.FirstOrDefault(a => a is ObsoleteAttribute) is ObsoleteAttribute;
                            assemblyModulo.AssemblyFullPath = file.FullName;
                            assemblyModulo.Assembly = moduloType.FullName;
                            assemblyModulo.Categoria = categoria != null && categoria.IsCategoriaNamespace
                                ? moduloType.Namespace
                                : categoria?.Valor;
                            unit.WriteContext.Update(modulo);
                            atualizados.Add(assemblyModulo);
                        }
                        else {
                            // Inserindo modulo
                            modulo = new AssemblyModulo() {
                                Codigo = info.Codigo,
                                Nome = info.Nome,
                                CreatedOn = DateTime.Now,
                                IsObsoleto =
                                    attributes.FirstOrDefault(a => a is ObsoleteAttribute) is ObsoleteAttribute,
                                AssemblyFullPath = file.FullName,
                                Assembly = moduloType.FullName,
                                Categoria = categoria != null && categoria.IsCategoriaNamespace
                                    ? moduloType.Namespace
                                    : categoria?.Valor,
                            };
                            unit.WriteContext.Add(modulo);
                            adicionados.Add((AssemblyModulo)modulo);
                        }

#if DEBUG // TODO: Somente deve ser usado em uma diretiva de compilação
                        ////////////////
                        // Agrupamento
                        if (!String.IsNullOrEmpty(info.AgrupamentoCodigo)) {
                            var agrupamento = reader.GetByCodigo<Modulo>(info.AgrupamentoCodigo);
                            if (agrupamento == null) {
                                agrupamento = new Modulo {
                                    Codigo = info.AgrupamentoCodigo,
                                    Nome = "Agrupamento - " + info.Nome,
                                    CreatedOn = DateTime.Now,
                                    Categoria = String.Empty,
                                    IsAgrupamento = true,
                                };
                                unit.WriteContext.Add(agrupamento);
                            }

                            if (agrupamento.AgrupamentoOrdenacaoIn == null || agrupamento.AgrupamentoOrdenacaoIn.All(o => o.ModuloExecutor.Codigo != modulo.Codigo)) {
                                var ordems =
                                    new List<ModuloOrdem>(agrupamento.AgrupamentoOrdenacaoIn ?? new ModuloOrdem[] {})
                                    {
                                        new ModuloOrdem()
                                        {
                                            ModuloAgrupamento = agrupamento,
                                            CreatedOn = DateTime.Now,
                                            Ordem = info.AgrupamentoOrdem,
                                            IsPrincipal = info.PrincipalNoAgrupamento,
                                            ModuloExecutor = modulo
                                        }
                                    };
                                agrupamento.AgrupamentoOrdenacaoIn = ordems;
                                unit.WriteContext.Update(agrupamento);
                            }
                        }
#endif
                        tr.Commit();
                    }
                }
                OnProcessed?.Invoke(this, new AtualizacaoAssemblyModuloEventArgs(e.FileName, adicionados, atualizados));
            }
            catch (ReflectionTypeLoadException ex) 
            {
                var loaderExceptions = ex.LoaderExceptions;
                throw loaderExceptions[0];
            }
        }
    }
}
