using System;
using System.Collections.Generic;
using System.Linq;
using Modular.Core.Repositories;
using Modular.CoreShared;
using Modular.Shared;
using Modular.Shared.Contracts;
using Modular.Shared.Data;
using Modular.Shared.Data.Business;
using Modular.Shared.Data.Business.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Modular.Core
{
    public class SupportCore : ISupport
    {
        private readonly IDataBisRead _bisRead;
        private readonly IDataBisWrite _bisWrite;
        private readonly IMapper _mapper;

        public SupportCore(IDataBisRead bisRead, IDataBisWrite bisWrite, IMapper mapper)
        {
            _bisRead = bisRead;
            _bisWrite = bisWrite;
            _mapper = mapper;
        }

        #region Resultado

        public IResultado GetSucessResult<TValue>(TValue resultValue, IContractResolver resolver = null, params JsonConverter[] converters)
        {
            if (resolver == null && _mapper != null)
            {
                resolver = new CoreContractResolver(_mapper);
            }
            var result = new Resultado {
                ResultValue = resultValue,
                ResultValueResolver = resolver,
                ResultValueConverters = converters
            };
            return result;
        }

        //public IResultado GetErrorResult(string messageErro)
        //{
        //    var result = new Resultado {
        //        IsException = false,
        //        ResultMessage = messageErro,
        //    };
        //    return result;
        //}

        public IResultado GetEntryParserResultError(string messageErro)
        {
            var result = new Resultado {
                IsException = false,
                IsEntryParserError = true,
                ResultMessage = messageErro,
            };
            return result;
        }

        public IResultado GetValidacaoResultError<TResultValue>(IEnumerable<ResultadoMessage> errors, TResultValue resultValue, IContractResolver resolver = null, params JsonConverter[] converters)
        {
            if (resolver == null && _mapper != null) {
                resolver = new CoreContractResolver(_mapper);
            }
            var result = new Resultado {
                IsException = false,
                IsValidacaoError = true,
                Messages = errors.ToArray(),
                ResultValue = resultValue,
                ResultValueResolver = resolver,
                ResultValueConverters = converters
            };
            return result;
        }

        public IResultado GetBusinessResultError<TResultValue>(ResultadoMessage message, TResultValue resultValue,
            IContractResolver resolver = null, params JsonConverter[] converters)
        {
            if (resolver == null && _mapper != null) {
                resolver = new CoreContractResolver(_mapper);
            }
            var result = new Resultado {
                IsBusinessError = true,
                Messages = new[] { message },
                ResultValue = resultValue,
                ResultValueResolver = resolver,
                ResultValueConverters = converters
            };
            return result;
        }

        public IResultado GetBusinessResultError<TResultValue>(string message, TResultValue resultValue,
            IContractResolver resolver = null, params JsonConverter[] converters)
        {
            throw new NotImplementedException();
        }

        //public IResultado GetExceptionResult(string messageErro, Exception ex)
        //{
        //    return Resultado.Exception(messageErro, ex);
        //}

        #endregion


        public IContractResolver GetContractResolver(IMapper mapper)
        {
            return new CoreContractResolver(mapper);
        }

        public IMapperManager GetMapperManager()
        {
            return new DataMapperManager();
        }

        public ICategoriaProdutoRepository GetCategoriaProdutoRepository()
        {
            return new CategoriaProdutoRepository(_bisRead, _bisWrite, _mapper);
        }

        public IProdutoRepository GetProdutoRepository()
        {
            return new ProdutoRepository(_bisRead, _bisWrite, _mapper);
        }

        public IClienteRepository GetClienteRepository()
        {
            return new ClienteRepository(_bisRead, _bisWrite, _mapper);
        }

        public IVendaRepository GetVendaRepository()
        {
            return new VendaRepository(_bisRead, _bisWrite, _mapper);
        }
    }
}