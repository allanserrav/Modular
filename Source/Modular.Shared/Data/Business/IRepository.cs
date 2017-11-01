using System.Collections.Generic;

namespace Modular.Shared.Data.Business
{
    public interface IRepository<TBis>
        where TBis : IDataBis
    {
        TBis Get(string id);
        TBis GetByCodigo(string codigo);
        IEnumerable<TBis> FindAll();
        bool Salvar(TBis data);
    }
}