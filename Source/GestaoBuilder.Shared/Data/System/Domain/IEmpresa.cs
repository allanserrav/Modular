namespace GestaoBuilder.Shared.Data.System.Domain
{
    public interface IEmpresa : IDataSys
    {
        string ConnectionStringDb { get;  }
        string DatabaseName { get;  }
        IEmpresa EmpresaPai { get;  }
        IEmpresa EmpresaMaster { get; }
    }
}