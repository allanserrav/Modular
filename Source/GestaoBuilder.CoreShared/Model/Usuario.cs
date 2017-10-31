using GestaoBuilder.CoreShared.Bases;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Data.System.Domain;

namespace GestaoBuilder.CoreShared.Model
{
    public class Usuario : BaseEntidade, IUsuario
    {
        public int SegundosLoginExpirar { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public IEmpresa EmpresaEmUSo { get; set; }
    }
}
