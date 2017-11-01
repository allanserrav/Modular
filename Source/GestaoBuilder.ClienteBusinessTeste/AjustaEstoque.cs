using System.Data.Common;
using Modular.ClienteBusinessTeste.Domain;
using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data;
using Modular.Shared.Data.Business;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.System.Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Modular.ClienteBusinessTeste
{
    [ModuloInfo(Codigo = "ABC001", Nome = "Ajustar o estoque")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class AjustaEstoque : IModuloContext
    {
        private IElapsedTimeWatch _time;
        private IDataBisRead _reader;
        private ILogger _logger;
        private IModuloService _service;
        private ISupport _support;
        private IDataBisWrite _writer;
        private IDataMapper _mapper;

        public void AddReader(IDataBisRead reader)
        {
            this._reader = reader;
        }

        public void AddWriter(IDataBisWrite writer)
        {
            this._writer = writer;
        }

        public void AddLogger(ILogger logger)
        {
            this._logger = logger;
        }

        public void AddService(IModuloService service)
        {
            this._service = service;
        }

        public void AddMappers(IDataMapper mapper)
        {
            this._mapper = mapper;
        }

        public void AddElapsedTime(IElapsedTimeWatch time)
        {
            this._time = time;
        }

        public void AddSupport(ISupport support)
        {
            this._support = support;
        }

        public IResultado Execute(IEntrada entry, IModulo modulo)
        {
            var contractResolver = _support.GetContractResolver(_mapper.Get<Estoque>());
            var ajustamento = entry.Parse<EstoqueAjustado>(contractResolver).Result;
            ajustamento.Ajuste = "Teste ajustamento";
            return _support.GetSucessResult(ajustamento);
        }
    }
}
