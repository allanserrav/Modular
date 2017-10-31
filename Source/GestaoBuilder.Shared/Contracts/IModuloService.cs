using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GestaoBuilder.Shared.Data.Business;

namespace GestaoBuilder.Shared.Contracts
{
    public interface IModuloService
    {
        //Task<IResultado> ExecutarModulo(string modulo, IEntry entry);

        //IResultado ExecutarModulo(string modulo, JToken entry);
        IResultado ExecutarModulo<TData>(string modulo, TData data)
            where TData : class, IDataBis;
    }
}
