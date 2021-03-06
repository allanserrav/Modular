﻿using System;
using System.Collections.Generic;
using System.Text;
using Modular.Shared.Atributos;

namespace Modular.Shared.Data.Business.Domain
{
    [Data(DocumentName = "cliente_creditos")]
    public class ClienteCredito : IDataBisKey
    {
        public DateTime CreatedOn { get; set; }
        public string Id { get; set; }
        //public Cliente Cliente { get; set; }
        public decimal Valor { get; set; }
        public bool Usado { get; set; }
    }
}
