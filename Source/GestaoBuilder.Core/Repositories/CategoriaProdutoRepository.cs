using System;
using System.Collections.Generic;
using System.Text;
using Modular.CoreShared.Bases;
using Modular.Shared;
using Modular.Shared.Data;
using Modular.Shared.Data.Business;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.Business.Repositories;

namespace Modular.Core.Repositories
{
    public class CategoriaProdutoRepository : BaseRepository<CategoriaProduto>, ICategoriaProdutoRepository
    {
        public CategoriaProdutoRepository(IDataBisRead reader, IDataBisWrite writer, IMapper mappers) : base(reader, writer, mappers)
        {
        }
    }
}
