using System;
using Modular.Shared.Atributos;

namespace Modular.Shared.Data.Business.Domain
{
    [Data(DocumentName = "vendas")]
    public class Venda : DataModificada<Venda>, IDataBis
    {
        public string Codigo { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDesabilitado { get; set; }
        public string Id { get; set; }

        public Cliente Cliente
        {
            get => Getter(c => c.Cliente);
            set => Setter(c => c.Cliente, value);
        }

        public ItemVenda[] Itens
        {
            get => Getter(c => c.Itens);
            set => Setter(c => c.Itens, value);
        }
        /// <summary>
        /// Valor total com todos os calculos, descontos etc...
        /// </summary>
        public decimal ValorTotalCalculado { get; set; }
        /// <summary>
        /// Valor de todos os itens sem realização de calculos
        /// </summary>
        public decimal ValorTotalSemCalculo { get; set; }

        [Componente(Codigo = "COMPVEN010")]
        public string Anotacao { get; set; }
    }
}
