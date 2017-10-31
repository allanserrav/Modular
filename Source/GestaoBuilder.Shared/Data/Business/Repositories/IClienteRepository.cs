using System.Collections.Generic;
using System.Linq;
using GestaoBuilder.Shared.Data.Business.Domain;

namespace GestaoBuilder.Shared.Data.Business.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        void AtualizarPontuacao(string id, int novaPontuacao);

        IEnumerable<Cliente> ListarClientesComCredito();
    }
}