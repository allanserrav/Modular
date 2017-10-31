using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.Business.Domain;
using GestaoBuilder.Shared.Data.System.Domain;

namespace GestaoBuilder.CoreBusiness.ClienteBusiness
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