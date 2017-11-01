namespace Modular.CoreBusiness
{
    public class BusinessModuloCodigo
    {
        private BusinessModuloCodigo() { }

        #region Agrupamento

        public const string AgrupamentoMapeamento = "AGR001";
        public const string AgrupamentoSalvaCategoriaProduto = "AGR101";
        public const string AgrupamentoSalvaProduto = "AGR102";
        public const string AgrupamentoSalvaCliente = "AGR103";
        public const string AgrupamentoRealizaVenda = "AGR104";

        #endregion

        #region Mapeamento
        public const string ConfiguraMapper = "CFM001";
        public const string ConfiguraClienteEntryMapper = "CFM002";
        #endregion

        #region Exemplos

        public const string ExemploCalcularFibonacci = "EXE001";
        public const string ExemploCalcularMedia = "EXE002";
        public const string ExemploRelatorioAposentados = "EXE003";
        public const string ExemploTestarChamadaServico_1 = "EXE004";
        public const string ExemploTestarChamadaServico_2 = "EXE005";
        public const string ExemploTestarChamadaServico_3 = "EXE006";
        public const string ExemploTestarChamadaServico_4 = "EXE007";
        public const string ExemploTestarChamadaServico_5 = "EXE008";
        public const string ExemploPreTestarAgrupamentoSimples = "EXE009";
        public const string ExemploTestarAgrupamentoSimples = "EXE010";

        #endregion

        #region Estoque

        public const string SalvaEstoque = "ESTO001";
        public const string ValidacaoEstoque = "ESTO002";
        public const string EntradaEstoque = "ESTO003";

        #endregion

        #region Produto

        public const string SalvaProduto = "PROD001";
        public const string ValidacaoProduto = "PROD002";
        public const string EntradaProduto = "PROD003";
        public const string ListaProduto = "PROD004";

        #endregion

        #region Categoria Produto

        public const string SalvaCategoriaProduto = "CAT001";
        public const string ValidacaoCategoriaProduto = "CAT002";
        public const string EntradaCategoriaProduto = "CAT003";
        public const string ListaCategoriaProduto = "CAT004";

        #endregion

        #region Cliente

        public const string SalvaCliente = "CLI001";
        public const string ValidacaoCliente = "CLI002";
        public const string AdicionaPontuacaoCliente = "CLI003";
        public const string ListaCliente = "CLIL004";

        #endregion

        #region Venda

        public const string RealizaVenda = "VEN001";
        public const string ValidacaoVendaEntry = "VEN002";
        public const string EntradaVenda = "VEN003";
        public const string ListaVenda = "VEN004";

        #endregion
    }
}
