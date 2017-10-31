using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using GestaoBuilder.CoreShared.Bases;
using GestaoBuilder.CoreShared.Contracts;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.Business;
using GestaoBuilder.Shared.Data.Business.Domain;
using GestaoBuilder.Shared.Data.Business.Repositories;

namespace GestaoBuilder.Core.Repositories
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