using Modular.Shared.Data.Business.Domain;

namespace Modular.ClienteBusinessTeste.Domain
{
    public class ProdutoAjustado : Produto
    {
        public int ParamAjuste { get; set; }
        public string Ajuste { get; set; }
    }
}