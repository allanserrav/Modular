using Modular.CoreShared.Bases;
using Modular.Shared;
using Modular.Shared.Data.System.Domain;

namespace Modular.CoreShared.Model
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
