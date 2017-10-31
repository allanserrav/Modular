using System;
using GestaoBuilder.Shared.Atributos;

namespace GestaoBuilder.Shared.Data.Business.Domain
{
    [Data(DocumentName = "cliente_pontos")]
    public class ClientePonto : IDataBisKey
    {
        public DateTime CreatedOn { get; set; }
        public string Id { get; set; }
        //public Cliente Cliente { get; set; }
        public decimal Pontos { get; set; }
        public bool GerouCredito { get; set; }
    }
}