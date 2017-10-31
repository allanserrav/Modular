using GestaoBuilder.CoreShared.Bases;
using GestaoBuilder.Shared;
using GestaoBuilder.Shared.Data.System.Domain;

namespace GestaoBuilder.CoreShared.Model
{
    public class Pagina : BaseEntidade, IPagina
    {
        public string Nome { get; set; }

        public string Observacao { get; set; }

        public int PaginaAnteriorId { get; set; }
        public IPagina PaginaAnterior { get; set; }

        public int PaginaProximaId { get; set; }
        public IPagina PaginaProxima { get; set; }

        public string HtmlPath { get; set; }
    }
}
