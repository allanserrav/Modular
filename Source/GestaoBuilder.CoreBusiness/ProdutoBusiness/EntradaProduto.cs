﻿using System;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.Business.Domain;
using GestaoBuilder.Shared.Data.System.Domain;

namespace GestaoBuilder.CoreBusiness.ProdutoBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.EntradaProduto, Nome = "Entrada de dados do produto")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class EntradaProduto : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            var result = entry.Parse<Produto>(Mapper);
            if (!result.IsParseSucess)
            {
                return Support.GetEntryParserResultError("Erro mapeando o objeto de entrada para produto");
            }
            var produto = result.Result;
            produto.Classe = ClasseProduto.Servico; // Forçado para serviço, não terá estoque a principio

            if (produto.Categoria != null && String.IsNullOrEmpty(produto.Categoria.Id))
            {
                var repository = Support.GetCategoriaProdutoRepository();
                produto.Categoria = repository.GetByCodigo(produto.CategoriaCodigo);
            }
            return Support.GetSucessResult(produto);
        }
    }
}