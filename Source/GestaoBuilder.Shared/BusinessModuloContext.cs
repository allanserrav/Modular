using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.Business;
using GestaoBuilder.Shared.Data.System.Domain;
using Microsoft.Extensions.Logging;

namespace GestaoBuilder.Shared
{
    public abstract class BusinessModuloContext : IModuloContext
    {
        public ILogger Logger { get; private set; }
        public IDataBisRead Reader { get; private set; }
        public IModuloService Service { get; private set; }
        public IElapsedTimeWatch ElapsedTime { get; private set; }
        public IDataBisWrite Writer { get; private set; }
        public ISupport Support { get; private set; }
        public IMapper Mapper { get; private set; }
        public IModulo Modulo { get; private set; }

        public void AddLogger(ILogger logger)
        {
            this.Logger = logger;
        }

        public void AddReader(IDataBisRead reader)
        {
            this.Reader = reader;
        }

        public void AddService(IModuloService service)
        {
            this.Service = service;
        }

        public void AddMappers(IMapper mapper)
        {
            this.Mapper = mapper;
        }

        public void AddElapsedTime(IElapsedTimeWatch time)
        {
            this.ElapsedTime = time;
        }

        public void AddSupport(ISupport support)
        {
            this.Support = support;
        }

        public void AddModuto(IModulo modulo)
        {
            this.Modulo = modulo;
        }

        public void AddWriter(IDataBisWrite writer)
        {
            this.Writer = writer;
        }

        public abstract IResultado Execute(IEntrada entry);

    }
}
