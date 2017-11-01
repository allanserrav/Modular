using System;
using System.Collections.Generic;
using System.Text;
using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.System.Domain;
using Modular.CoreBusiness.CategoriaProdutoBusiness.Validators;

namespace Modular.CoreBusiness.CategoriaProdutoBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.EntradaCategoriaProduto, Nome = "Entrada de dados da categoria de produto")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class EntradaCategoriaProduto : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            var parseResult = entry.Parse<CategoriaProduto>(Mapper);
            if(!parseResult.IsParseSucess)
            {
                return Support.GetEntryParserResultError("Dados de entrada inválidos");
            }

            var categoria = parseResult.Result;

            if (categoria.CategoriaPai != null && String.IsNullOrEmpty(categoria.CategoriaPai.Id))
            {
                var repository = Support.GetCategoriaProdutoRepository();
                categoria.CategoriaPai = repository.GetByCodigo(categoria.CategoriaPai.Codigo);
            }

            /////////////////
            // Validação
            var validator = new CategoriaProdutoValidator(entry.TipoOperacao);
            var resultValidator = validator.Validate(categoria);
            if(!resultValidator.IsValid)
            {
                var errors = resultValidator.Errors.GetValidacaoMessage();
                return Support.GetValidacaoResultError(errors, categoria);
            }
            /////////////////

            return Support.GetSucessResult(categoria);
        }
    }
}
