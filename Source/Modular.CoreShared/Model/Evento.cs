using Modular.CoreShared.Bases;
using Modular.Shared;

namespace Modular.CoreShared.Model
{
    public enum EventoType
    {
        Indefinido,
        OnLoad = 1,
        OnAction,
        OnValidate,
    }

    public class Evento : BaseEntidade
    {

    }
}
