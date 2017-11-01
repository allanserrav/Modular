using Modular.CoreShared.Bases;
using Modular.Shared.Data;
using Modular.Shared.Data.Business;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.Business.Repositories;

namespace Modular.Core.Repositories
{
    public class VendaRepository : BaseRepository<Venda>, IVendaRepository
    {
        public VendaRepository(IDataBisRead reader, IDataBisWrite writer, IMapper mapper) : base(reader, writer, mapper)
        {
        }
    }
}