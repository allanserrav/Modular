using System;
using System.Collections.Generic;
using System.Text;
using GestaoBuilder.CoreShared.Model;
using GestaoBuilder.Shared.Data;
using GestaoBuilder.Shared.Data.System;

namespace GestaoBuilder.Core
{
    public class ModuloReader
    {
        private readonly IDataRead<IDataSysKey> _reader;

        public ModuloReader(IDataRead<IDataSysKey> reader)
        {
            _reader = reader;
        }

        public Modulo Get(string codigo)
        {
            return _reader.GetByCodigo<Modulo>(codigo);
        }
    }
}
