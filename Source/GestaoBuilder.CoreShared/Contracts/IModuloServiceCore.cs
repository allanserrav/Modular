using System.Threading.Tasks;
using GestaoBuilder.Shared.Contracts;

namespace GestaoBuilder.CoreShared.Contracts
{
    public interface IModuloServiceCore : IModuloService
    {
        Task<IResultado> ExecutarModuloCore(string modulo, IEntrada entry);
    }
}