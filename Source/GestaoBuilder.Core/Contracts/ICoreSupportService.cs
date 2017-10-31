using GestaoBuilder.CoreShared.Contracts;
using GestaoBuilder.CoreShared.Model;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.System.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoBuilder.Core.Contracts
{
    public interface ICoreSupportService : ICoreSupport
    {
        void AddUsuarioLogado(Usuario usuario);
        void AddUsuarioEntry(Usuario usuario);
        void AddMapper(IMapper mapper);
    }
}
