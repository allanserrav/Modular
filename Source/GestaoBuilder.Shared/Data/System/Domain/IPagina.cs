namespace GestaoBuilder.Shared.Data.System.Domain
{
    public interface IPagina
    {
        string Nome { get; set; }

        string Observacao { get; set; }

        int PaginaAnteriorId { get; set; }
        IPagina PaginaAnterior { get; set; }

        int PaginaProximaId { get; set; }
        IPagina PaginaProxima { get; set; }

        string HtmlPath { get; set; }
    }
}