namespace GestaoBuilder.Shared.Data.System.Domain
{
    public interface IUsuario : IDataSys
    {
        int SegundosLoginExpirar { get; }
        string Nome { get; }
        string Senha { get; }
        IEmpresa EmpresaEmUSo { get; set; }
    }
}