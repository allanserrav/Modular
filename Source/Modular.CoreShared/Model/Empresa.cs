using System;
using Modular.Shared.Data.System;
using Modular.Shared.Data.System.Domain;

namespace Modular.CoreShared.Model
{
    public class Empresa : IEmpresa
    {
        public string Codigo { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDesabilitado { get; set; }
        public int Id { get; set; }
        public string ConnectionStringDb { get; set; }
        public string DatabaseName { get; set; }
        public IEmpresa EmpresaPai { get; set; }
        public IEmpresa EmpresaMaster { get; set; }
    }
}