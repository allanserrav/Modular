using System.Threading.Tasks;
using Modular.Shared.Contracts;

namespace Modular.CoreShared.Contracts
{
    public interface IModuloServiceCore : IModuloService
    {
        Task<IResultado> ExecutarModuloCore(string modulo, IEntrada entry);
    }
}