using System;

namespace GestaoBuilder.Shared.Data
{
    public interface IDataKey
    {
        
    }

    public interface IBaseData
    {
        string Codigo { get; set; }

        DateTime CreatedOn { get; set; }

        DateTime? UpdatedOn { get; set; }

        bool IsDesabilitado { get; set; }
    }
}