using System;
using System.Collections.Generic;
using System.Text;
using GestaoBuilder.CoreShared.Bases;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.Business;
using GestaoBuilder.Shared.Data.Business.Domain;
using GestaoBuilder.Shared.Data.Business.Repositories;

namespace GestaoBuilder.Core.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(IDataBisRead reader, IDataBisWrite writer, IMapper mappers) : base(reader, writer, mappers)
        {
        }
    }
}
