using System.Linq;
using Modular.CoreBusiness.ProdutoBusiness.Validators;
using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data.Business.Domain;

namespace Modular.CoreBusiness.ProdutoBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.ValidacaoProduto, Nome = "Validação do produto")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class ValidacaoProduto : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            if (entry.ResultadoAnterior != null && entry.ResultadoAnterior.IsEntryParserError)
            {
                return entry.ResultadoAnterior;
            }

            var produto = entry.Parse<Produto>(Mapper).Result;
            var validator = new ProdutoValidator();
            var result = validator.Validate(produto);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(c => new ResultadoMessage { ChaveMessage = c.ErrorCode, Message = c.ErrorMessage });
                return Support.GetValidacaoResultError(errors, produto);
            }
            return Support.GetSucessResult(produto);
        }
    }
}