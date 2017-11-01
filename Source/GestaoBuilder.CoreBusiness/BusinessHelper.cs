using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Modular.Shared.Contracts;

namespace Modular.CoreBusiness
{
    public static class BusinessHelper
    {
        public static IEnumerable<ResultadoMessage> GetValidacaoMessage(string message)
        {
            return new [] { new ResultadoMessage { ChaveMessage = "", Message = message, TipoMessage = TipoMessageEnum.Validacao} };
        }

        public static IEnumerable<ResultadoMessage> GetValidacaoMessage(this IList<ValidationFailure> errors)
        {
            return errors.Select(c => new ResultadoMessage {ChaveMessage  = c.ErrorCode, Message = c.ErrorMessage, TipoMessage = TipoMessageEnum.Validacao});
        }
    }
}