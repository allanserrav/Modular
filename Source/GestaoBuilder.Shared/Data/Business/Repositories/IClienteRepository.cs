using System.Collections.Generic;
using System.Linq;
using Modular.Shared.Data.Business.Domain;

namespace Modular.Shared.Data.Business.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        void AtualizarPontuacao(string id, int novaPontuacao);

        IEnumerable<Cliente> ListarClientesComCredito();
    }
}