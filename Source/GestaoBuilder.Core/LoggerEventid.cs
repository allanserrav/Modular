using Microsoft.Extensions.Logging;

namespace Modular.Core
{
    public static class LoggerEventId
    {
        public static int CompareTo(this EventId x, EventId y)
        {
            return x.Id.CompareTo(y.Id);
        }

        public static EventId TempoDeExecucao => new EventId(101, "Tempo de execucao");
        public static EventId AguardouExecucao => new EventId(102, "Aguardou tempo de execucao");
        public static EventId OcorreuErro => new EventId(1001, "Ocorreu um erro");
    }
}
