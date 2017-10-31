using System;

namespace GestaoBuilder.Shared.Data.Business.Domain
{
    public class ItemVenda : IDataBisKey
    {
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public Produto Produto { get; set; }
        public int QuantidadePedida { get; set; }
        public decimal ValorDeVenda { get; set; }
        public Desconto Desconto { get; set; }
    }
}