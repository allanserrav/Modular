using GestaoBuilder.CoreShared.Bases;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.Business;
using GestaoBuilder.Shared.Data.Business.Domain;
using GestaoBuilder.Shared.Data.Business.Repositories;

namespace GestaoBuilder.Core.Repositories
{
    public class VendaRepository : BaseRepository<Venda>, IVendaRepository
    {
        public VendaRepository(IDataBisRead reader, IDataBisWrite writer, IMapper mapper) : base(reader, writer, mapper)
        {
        }
    }
}