using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Modular.Shared.Data;
using Modular.Shared.Data.Business;
using Modular.Shared.Data.System.Domain;

namespace Modular.Shared.Contracts
{
    public interface IModuloPlan
    {
        IResultado Resultado { get; }

        TimeSpan ExecutionTime { get; }
    }

    public interface IModuloContext
    {
        void AddReader(IDataBisRead reader);

        void AddWriter(IDataBisWrite writer);

        void AddLogger(ILogger logger);

        void AddService(IModuloService service);

        void AddMappers(IMapper mapper);

        void AddElapsedTime(IElapsedTimeWatch time);
        void AddSupport(ISupport support);
        void AddModuto(IModulo modulo);

        IResultado Execute(IEntrada entry);
    }
}
