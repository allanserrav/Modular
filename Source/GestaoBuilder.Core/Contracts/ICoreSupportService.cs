using Modular.CoreShared.Contracts;
using Modular.CoreShared.Model;
using Modular.Shared.Data;
using Modular.Shared.Data.System.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modular.Core.Contracts
{
    public interface ICoreSupportService : ICoreSupport
    {
        void AddUsuarioLogado(Usuario usuario);
        void AddUsuarioEntry(Usuario usuario);
        void AddMapper(IMapper mapper);
    }
}
