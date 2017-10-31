using GestaoBuilder.CoreShared.Bases;
using GestaoBuilder.Shared;

namespace GestaoBuilder.CoreShared.Model
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
