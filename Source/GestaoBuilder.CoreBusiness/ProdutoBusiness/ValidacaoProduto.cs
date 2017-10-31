using System.Linq;
using GestaoBuilder.CoreBusiness.ProdutoBusiness.Validators;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.Business.Domain;

namespace GestaoBuilder.CoreBusiness.ProdutoBusiness
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