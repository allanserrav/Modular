using System;
using System.Collections.Generic;
using System.Text;
using Modular.CoreShared.Contracts;
using Modular.CoreShared.Model;
using Modular.Shared.Data;

namespace Modular.Core
{
    public class CoreSupportService : ICoreSupport
    {
        public Usuario UsuarioLogado { get; set; }
        public Usuario UsuarioEntry { get; set; }
        public IMapper Mapper { get; set; }
    }
}
