using System.Threading.Tasks;
using Modular.Shared.Contracts;
using Newtonsoft.Json.Linq;

namespace Modular.Shared.Data.Business
{
    public interface IDataBisWrite : IDataWrite<IDataBisKey>
    {
        void AddFromJson(JToken json);
    }
}