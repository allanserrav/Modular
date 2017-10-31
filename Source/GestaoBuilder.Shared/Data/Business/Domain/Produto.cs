using System;
using GestaoBuilder.Shared.Atributos;

namespace GestaoBuilder.Shared.Data.Business.Domain
{
    public enum ClasseProduto
    {
        Servico = 1,
        Estoque,
    }

    [Data(DocumentName = "produtos")]
    public class Produto : IDataBis
    {
        public string Codigo { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDesabilitado { get; set; }
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Anotacao { get; set; }
        //public int QuantidadeMinimaEstoque { get; set; }
        //public int QuantidadeMaximaEstoque { get; set; }
        public ClasseProduto Classe { get; set; }
        public decimal Preco { get; set; }
        public CategoriaProduto Categoria { get; set; }
        public string CategoriaCodigo { get; set; }
    }
}