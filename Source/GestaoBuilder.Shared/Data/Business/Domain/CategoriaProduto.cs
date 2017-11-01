using System;
using System.Collections.Generic;
using System.Text;
using Modular.Shared.Atributos;

namespace Modular.Shared.Data.Business.Domain
{
    [Data(DocumentName = "categorias_produto")]
    public class CategoriaProduto : IDataBis
    {
        public string Codigo { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDesabilitado { get; set; }
        public string Id { get; set; }
        public string Descricao { get; set; }
        public string Anotacao { get; set; }
        public CategoriaProduto CategoriaPai { get; set; }
    }
}
