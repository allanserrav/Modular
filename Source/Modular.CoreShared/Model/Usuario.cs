using Modular.CoreShared.Bases;
using Modular.Shared;
using Modular.Shared.Data.System.Domain;

namespace Modular.CoreShared.Model
{
    public class Usuario : BaseEntidade, IUsuario
    {
        public int SegundosLoginExpirar { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public IEmpresa EmpresaEmUSo { get; set; }
    }
}
