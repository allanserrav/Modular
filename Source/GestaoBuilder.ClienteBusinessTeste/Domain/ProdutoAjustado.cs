using GestaoBuilder.Shared.Data.Business.Domain;

namespace GestaoBuilder.ClienteBusinessTeste.Domain
{
    public class ProdutoAjustado : Produto
    {
        public int ParamAjuste { get; set; }
        public string Ajuste { get; set; }
    }
}