using System;
using System.Collections.Generic;
using Modular.Shared;
using Modular.Shared.Data;
using Modular.Shared.Data.Business;

namespace Modular.CoreShared.Bases
{
    public class BaseRepository<TBis> : IRepository<TBis>
        where TBis : class, IDataBis
    {
        public IDataBisRead Reader { get; }
        public IDataBisWrite Writer { get; }

        private readonly IMapper _mapper;

        public BaseRepository(IDataBisRead reader, IDataBisWrite writer, IMapper mapper)
        {
            Reader = reader;
            Writer = writer;
            _mapper = mapper;
        }

        public TBis Get(string id)
        {
            return Reader.Get<TBis>(id);
        }

        public TBis GetByCodigo(string codigo)
        {
            return Reader.GetByCodigo<TBis>(codigo);
        }

        public bool Salvar(TBis data)
        {
            data.CreatedOn = data.CreatedOn.Equals(DateTime.MinValue) ? DateTime.Now : data.CreatedOn;
            if (String.IsNullOrEmpty(data.Id)) // Add
            {
                Writer.Add(data);
            }
            else
            {
                data.UpdatedOn = DateTime.Now;
                Writer.Update(data);
            }
            return true;
        }

        public IEnumerable<TBis> FindAll()
        {
            return Reader.GetJsonQuery<TBis>(null);
        }
    }
}
