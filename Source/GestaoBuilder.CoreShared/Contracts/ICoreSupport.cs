using Modular.CoreShared.Model;
using Modular.Shared.Data;

namespace Modular.CoreShared.Contracts
{
    public interface ICoreSupport
    {
        Usuario UsuarioLogado { get; }
        Usuario UsuarioEntry { get; }
        IMapper Mapper { get; }
    }
}