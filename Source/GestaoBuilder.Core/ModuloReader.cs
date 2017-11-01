using System;
using System.Collections.Generic;
using System.Text;
using Modular.CoreShared.Model;
using Modular.Shared.Data;
using Modular.Shared.Data.System;

namespace Modular.Core
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
