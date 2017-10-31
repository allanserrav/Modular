using System;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Atributos;
using GestaoBuilder.Shared.Contracts;
using GestaoBuilder.Shared.Data.System.Domain;

namespace GestaoBuilder.CoreBusiness.Exemplos
{
    [ModuloInfo(Codigo = BusinessModuloCodigo.ExemploTestarChamadaServico_4, Nome = "Testar chamada de modulo por serviço 4")]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class TestarChamadaServico_4 : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry, IModulo modulo)
        {
            throw new InvalidOperationException();
        }
    }
}
