using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GestaoBuilder.CoreShared;
using GestaoBuilder.CoreShared.Contracts;
using GestaoBuilder.CoreShared.Model;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.Business;
using Microsoft.Extensions.Logging;
using GestaoBuilder.Core.Contracts;

namespace GestaoBuilder.Core
{
    public class ModuloService : IModuloServiceCore
    {
        public ModuloService(ModuloReader reader, IScriptResource resource, ILoggerFactory loggerFactory, IDataBisRead bisRead, IDataBisWrite bisWrite, ICoreSupportService core)
        {
            Reader = reader;
            Resource = resource;
            Factory = loggerFactory;
            BisRead = bisRead;
            BisWrite = bisWrite;
            Core = core;
            Logger = loggerFactory.CreateLogger<ModuloService>();
            Plan = new ModuloPlanManager(this.Logger, 2); // TODO: Colocar a quantidade máxima de agrupamentos no arquivo de configuração
        }

        public ModuloReader Reader { get; }
        private IScriptResource Resource { get; }
        private ILoggerFactory Factory { get; }
        public IDataBisRead BisRead { get; }
        public IDataBisWrite BisWrite { get; }
        public ICoreSupportService Core { get; }
        private ILogger<ModuloService> Logger { get; }
        private ModuloPlanManager Plan { get; }
        private IEntrada EntradaOriginal { get; set; }

        public IResultado ExecutarModulo<TData>(string modulo, TData data) where TData : class, IDataBis
        {
            var entrada = new Entrada(EntradaOriginal.JsonOriginalEntry, EntradaOriginal.UsuarioEntry,
                EntradaOriginal.TipoOperacao) {ValorAtual = data};
            var result = ExecutarModuloCore(modulo, entrada);
            return result.GetAwaiter().GetResult();
        }

        public async Task<IResultado> ExecutarModuloCore(string moduloCodigo, IEntrada entry)
        {
            try {
                var modulo = Reader.Get(moduloCodigo);
                IResultado resultado = null;
                EntradaOriginal = EntradaOriginal ?? entry;
                await ObterDataMapeamento(entry);
                if (modulo.IsAgrupamento) {
                    resultado = await ExecutarAgrupamento(modulo, entry);
                }
                else {
                    resultado = await ExecutarModulo(modulo, entry);
                }
                return resultado;
            }
            catch (Exception ex) {
                return Resultado.Exception($"Uma exeção ocorreu na chamada do módulo {moduloCodigo}", ex);
            }
        }

        private async Task ObterDataMapeamento(IEntrada entry)
        {
            var modulo = Reader.Get("AGR001"); // TODO: Colocar o codigo de agrupamento do mapeamento no arquivo de configuração
            var result = await ExecutarAgrupamento(modulo, entry);
            Core.AddMapper(result.Parse<DataMapperManager>().Result);
        }

        private async Task<IResultado> ExecutarAgrupamento(Modulo agrupamento, IEntrada entry)
        {
            using (var time = new Timewatch()) {
                Modulo anterior = null;
                IResultado resultadoCorrente = null;
                IResultado resultadoPrincipal = null;
                IEntrada currentEntry = entry;
                int planId = Plan.AddModuloAgrupamento(agrupamento);
                foreach (var ordem in agrupamento.AgrupamentoOrdenacaoIn
                                            .OrderBy(o => o.Ordem)) {
                    ordem.ModuloExecutor.IsPrincipal = ordem.IsPrincipal;
                    resultadoCorrente = await ExecutarModulo(ordem.ModuloExecutor, currentEntry, anterior, agrupamento);
                    if (resultadoCorrente.IsException)
                    {
                        return Resultado.Error(
                            $"Ocorreu um erro inesperado na execução do módulo {ordem.ModuloExecutor.Codigo} no agrupamento {agrupamento.Codigo}",
                            resultadoCorrente);
                    }
                    if (ordem.IsPrincipal && resultadoCorrente.HasError) // Se modulo principal rodou com erro, retorna ele, e não deixa prosseguir.
                    {
                        return resultadoCorrente;
                    }
                    if (ordem.IsPrincipal) // Guarda o resultado do modulo principal, sem erro até aqui
                    {
                        resultadoPrincipal = resultadoCorrente;
                    }
                    else if (resultadoPrincipal != null && resultadoCorrente.HasError) // Se já passou do módulo principal, e houve erro posterior, retorno o resultado do módulo principal
                    {
                        return resultadoPrincipal;
                    }
                    currentEntry = new Entrada(resultadoCorrente, entry.JsonOriginalEntry, entry.UsuarioEntry, EntradaOriginal.TipoOperacao);
                    anterior = ordem.ModuloExecutor;
                }
                //var resultFinal = Resultado.Load(
                //    resultadoCorrente != null && resultadoCorrente.IsException ? resultadoCorrente : resultadoPrincipal,
                //    resultadoPrincipal != null && resultadoPrincipal.IsException,
                //    resultadoPrincipal.ResultMessage, null);
                Plan.AddResult(planId, resultadoCorrente, time.Elapsed);
                return resultadoCorrente;
            }
        }

        private async Task<IResultado> ExecutarModulo(Modulo modulo, IEntrada entry, Modulo anterior = null, Modulo agrupamento = null)
        {
            var engine = EngineFactory.GetInstance(modulo, Resource);
            using (Logger.BeginScope($"{ engine.GetType().Name }")) {
                engine.AddReader(BisRead);
                engine.AddWriter(BisWrite);
                engine.AddLogger(Logger);
                engine.AddService(this);
                engine.AddMappers(Core.Mapper);
                engine.AddSupport(new SupportCore(BisRead, BisWrite, Core.Mapper));
                modulo.AgrupamentoIn = agrupamento;
                modulo.AnteriorIn = anterior;
                engine.AddModuto(modulo);
                int planId = Plan.AddModulo(modulo);
                using (var time = new Timewatch()) {
                    engine.AddElapsedTime(time);
                    time.OnWaitFor = ((t) => {
                        Plan.AddEvent(planId, LoggerEventId.AguardouExecucao, t);
                    });
                    var result = await Task.Run(() =>
                    {
                        Exception exception = null;
                        IResultado resultado = null;
                        try
                        {
                            resultado = engine.Execute(entry);
                        }
                        catch (ReflectionTypeLoadException ex) {
                            var loaderExceptions = ex.LoaderExceptions;
                            exception= loaderExceptions[0];
                        }
                        catch (MissingMethodException ex)
                        {
                            // TODO: Esta excecao deve gerar um critical
                            exception = ex;
                        }
                        catch (Exception ex)
                        {
                            exception = ex;
                        }
                        if (exception != null)
                        {
                            Guid excode = Guid.Empty;
                            var resultException = Resultado.Exception(exception, out excode);
                            Plan.AddException(planId, time.Elapsed, exception); // TODO: incluir o excode aqui no AddException
                            return resultException;
                        }
                        return resultado;
                    });
                    Plan.AddResult(planId, result, time.Elapsed);
                    return result;
                }
            }
        }
    }
}
