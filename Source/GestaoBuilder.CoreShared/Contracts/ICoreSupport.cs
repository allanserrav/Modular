using GestaoBuilder.CoreShared.Model;
using GestaoBuilder.Shared.Data;

namespace GestaoBuilder.CoreShared.Contracts
{
    public interface ICoreSupport
    {
        Usuario UsuarioLogado { get; }
        Usuario UsuarioEntry { get; }
        IMapper Mapper { get; }
    }
}