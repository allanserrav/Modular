using System;
using Modular.CoreShared.Model;
using Modular.Shared.Data;
using Modular.Core.Contracts;
using Modular.Shared.Data.System.Domain;

namespace GestaoBuilder_WebCore
{
    public class WebCoreSupport : ICoreSupportService
    {
        public Usuario UsuarioLogado { get; set; }

        public Usuario UsuarioEntry { get; set; }

        public IMapper Mapper { get; set; }

        public void AddMapper(IMapper mapper)
        {
            Mapper = mapper;
        }

        public void AddUsuarioEntry(Usuario usuario)
        {
            UsuarioEntry = usuario;
        }

        public void AddUsuarioLogado(Usuario usuario)
        {
            UsuarioLogado = usuario;
        }
    }
}
