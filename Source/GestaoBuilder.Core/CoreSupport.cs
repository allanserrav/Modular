using System;
using System.Collections.Generic;
using System.Text;
using GestaoBuilder.CoreShared.Contracts;
using GestaoBuilder.CoreShared.Model;
using GestaoBuilder.Shared.Data;

namespace GestaoBuilder.Core
{
    public class CoreSupportService : ICoreSupport
    {
        public Usuario UsuarioLogado { get; set; }
        public Usuario UsuarioEntry { get; set; }
        public IMapper Mapper { get; set; }
    }
}
