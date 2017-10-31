using System;
using System.Collections.Generic;
using GestaoBuilder.CoreShared.Model;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Contracts;
using Microsoft.Extensions.Logging;

namespace GestaoBuilder.Core
{
    public class ModuloPlanManager
    {
        public int QuantidadeAgrupamentoMaximo { get; }
        public bool HasAgrupamento { get; private set; }
        public int QuantidadeAgrupamentos { get; private set; }
        public bool HasError { get; private set; }
        public Modulo ModuloError { get; private set; }

        private class ModuloPlan
        {
            public ModuloPlan()
            {
                Events = new List<ModuloPlanEvent>();
            }

            public int PlanId { get; set; }
            public Modulo Modulo { get; set; }
            public IResultado Resultado { get; set; }
            public TimeSpan TempoExecucao { get; set; }
            public bool GerouAExcecao { get; set; }
            public List<ModuloPlanEvent> Events { get; set; }
        }

        private class ModuloPlanEvent
        {
            public EventId EventId { get; set; }
            public object EventValue { get; set; }
        }

        private readonly List<ModuloPlan> lista;
        private readonly ILogger logger;

        public ModuloPlanManager(ILogger logger, int quantidadeAgrupamentoMaximo)
        {
            QuantidadeAgrupamentoMaximo = quantidadeAgrupamentoMaximo;
            this.logger = logger;
            lista = new List<ModuloPlan>();
        }

        public int AddModulo(Modulo modulo)
        {
            lista.Add(new ModuloPlan() { Modulo = modulo });
            logger.LogDebug($"Iniciando o modulo {modulo.Codigo}");
            return lista.Count;
        }

        public int AddModuloAgrupamento(Modulo agrupamento)
        {
            // Se atingir a quantidade máxima no plano de execução, deve gerar uma exceção
            if (QuantidadeAgrupamentos++ > QuantidadeAgrupamentoMaximo) throw new InvalidOperationException("Atingiu a quantidade máxima no plano de execução"); 
            lista.Add(new ModuloPlan() { Modulo = agrupamento });
            logger.LogDebug($"Iniciando o agrupamento {agrupamento.Codigo}");
            return lista.Count;
        }

        public void AddResult(int planId, IResultado resultado, TimeSpan time)
        {
            var moduloPlan = lista[planId - 1];
            moduloPlan.Resultado = resultado;
            moduloPlan.TempoExecucao = time;
            AddEvent(planId, LoggerEventId.TempoDeExecucao, time);
            logger.LogDebug($"Terminando o módulo {moduloPlan.Modulo.Codigo}");
        }

        public void AddException(int planId, TimeSpan time, Exception ex)
        {
            var moduloPlan = lista[planId - 1];
            if (!HasError) {
                // Modulo que gerou o erro
                moduloPlan.GerouAExcecao = true;
                HasError = true;
                ModuloError = moduloPlan.Modulo;
                AddEvent(planId, LoggerEventId.OcorreuErro, ex);
            }
            AddEvent(planId, LoggerEventId.TempoDeExecucao, time);
            logger.LogDebug($"Terminando o módulo {moduloPlan.Modulo.Codigo}");
        }

        public void AddEvent(int planId, EventId eventId, object eventValue)
        {
            var moduloPlan = lista[planId - 1];
            moduloPlan.Events.Add(new ModuloPlanEvent() { EventValue = eventValue, EventId = eventId, });
            string text = String.Empty;
            if (eventId.CompareTo(LoggerEventId.AguardouExecucao) == 0) {
                var time = (TimeSpan)eventValue;
                logger.LogDebug(eventId, $"Modulo {moduloPlan.Modulo.Codigo} aguardou {time.TotalMilliseconds} (ms) para continuar execução");
            }
            else if (eventId.CompareTo(LoggerEventId.TempoDeExecucao) == 0) {
                var time = (TimeSpan)eventValue;
                logger.LogDebug(LoggerEventId.TempoDeExecucao, $"Modulo {moduloPlan.Modulo.Codigo} executou em {time.TotalMilliseconds} (ms)");
            }
            else if (eventId.CompareTo(LoggerEventId.OcorreuErro) == 0) {
                var ex = (Exception)eventValue;
                logger.LogError(eventId, ex, $"Ocorreu um error na execução do modulo {moduloPlan.Modulo.Codigo}");
            }
        }
    }
}
