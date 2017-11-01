using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Modular.CoreShared.Bases;
using Modular.CoreShared.Contracts;
using Modular.Shared.Data;
using Modular.Shared.Data.Business;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.Business.Repositories;

namespace Modular.Core.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(IDataBisRead reader, IDataBisWrite writer, IMapper mapper) : base(reader, writer, mapper)
        {
        }

        public void AtualizarPontuacao(string id, int novaPontuacao)
        {
            Writer.Update(new Cliente {Id = id, PontuacaoAtual = novaPontuacao});
        }

        public IEnumerable<Cliente> ListarClientesComCredito()
        {
            return Reader.GetJsonQuery<Cliente>("{ codigo : {$eq : 'CLI001'} }");
        }
    }
}