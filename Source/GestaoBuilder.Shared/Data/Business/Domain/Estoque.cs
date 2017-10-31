using System;
using GestaoBuilder.Shared.Atributos;

namespace GestaoBuilder.Shared.Data.Business.Domain
{
    [Data(DocumentName = "estoques")]
    public class Estoque : IDataBis
    {
        public string Codigo { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDesabilitado { get; set; }
        public string Id { get; set; }
        public int Quantidade { get; set; }
        public string NomeTela { get; set; }
    }
}
