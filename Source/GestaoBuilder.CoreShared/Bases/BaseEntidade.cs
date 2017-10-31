using System;
using GestaoBuilder.Shared.Data.System;

namespace GestaoBuilder.CoreShared.Bases
{
    public class BaseEntidade : IDataSys
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsDesabilitado { get; set; }
    }
}
