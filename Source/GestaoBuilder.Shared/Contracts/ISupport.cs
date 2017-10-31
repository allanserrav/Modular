using System;
using System.Collections.Generic;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.Business;
using GestaoBuilder.Shared.Data.Business.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GestaoBuilder.Shared.Contracts
{
    public interface ISupport
    {
        #region Result

        IResultado GetSucessResult<TResultValue>(TResultValue resultValue, IContractResolver resolver = null, params JsonConverter[] converters);
        //IResultado GetErrorResult(string messageErro);
        IResultado GetEntryParserResultError(string messageErro);
        IResultado GetValidacaoResultError<TResultValue>(IEnumerable<ResultadoMessage> errors, TResultValue resultValue, IContractResolver resolver = null, params JsonConverter[] converters);
        IResultado GetBusinessResultError<TResultValue>(ResultadoMessage message, TResultValue resultValue, IContractResolver resolver = null, params JsonConverter[] converters);
        IResultado GetBusinessResultError<TResultValue>(string message, TResultValue resultValue, IContractResolver resolver = null, params JsonConverter[] converters);
        //IResultado GetExceptionResult(string messageErro, Exception ex);

        #endregion

        IContractResolver GetContractResolver(IMapper mapper);

        IMapperManager GetMapperManager();

        #region Repositories

        ICategoriaProdutoRepository GetCategoriaProdutoRepository();
        IProdutoRepository GetProdutoRepository();
        IClienteRepository GetClienteRepository();
        IVendaRepository GetVendaRepository();

        #endregion
    }
}