using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.System.Domain;

namespace Modular.CoreBusiness.ClienteBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.ListaCliente, Nome = "Listar os produtos")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class ListaCliente : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            //var filtro = entry.Parse<Cliente>(Mapper).Result;
            var repository = Support.GetClienteRepository();
            var p = repository.ListarClientesComCredito();

            return Support.GetSucessResult(p);
        }
    }
}