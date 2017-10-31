using System;

namespace GestaoBuilder.Shared.Data.Business.Domain
{
    public enum ClasseDesconto : byte
    {
        Vendedor = 1,
    }

    public class Desconto : IDataBisKey
    {
        public string Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public decimal ValorFixo { get; set; }
        public decimal ValorPercentual { get; set; }
        public ClasseDesconto Classe { get; set; }
    }
}