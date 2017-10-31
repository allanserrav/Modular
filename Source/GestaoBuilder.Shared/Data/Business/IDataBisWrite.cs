using System.Threading.Tasks;
using GestaoBuilder.Shared.Contracts;
using Newtonsoft.Json.Linq;

namespace GestaoBuilder.Shared.Data.Business
{
    public interface IDataBisWrite : IDataWrite<IDataBisKey>
    {
        void AddFromJson(JToken json);
    }
}