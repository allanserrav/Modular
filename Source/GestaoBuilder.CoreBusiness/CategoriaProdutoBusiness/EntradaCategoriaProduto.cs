using System;
using System.Collections.Generic;
using System.Text;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.Business.Domain;
using GestaoBuilder.Shared.Data.System.Domain;
using GestaoBuilder.CoreBusiness.CategoriaProdutoBusiness.Validators;

namespace GestaoBuilder.CoreBusiness.CategoriaProdutoBusiness
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
