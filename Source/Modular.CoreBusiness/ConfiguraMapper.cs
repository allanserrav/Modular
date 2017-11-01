using Modular.Shared;
using Modular.Shared.Atributos;
using Modular.Shared.Contracts;
using Modular.Shared.Data;
using Modular.Shared.Data.Business;
using Modular.Shared.Data.Business.Domain;
using Modular.Shared.Data.System.Domain;
using Newtonsoft.Json;

namespace Modular.CoreBusiness
{
    [Modulo(Codigo = BusinessModuloCodigo.ConfiguraMapper, Nome = "Configuraçào dos mapeamentos de data",
                AgrupamentoCodigo = BusinessModuloCodigo.AgrupamentoMapeamento, AgrupamentoOrdem = 1, PrincipalNoAgrupamento = true)]
    [ModuloCategoria(IsCategoriaNamespace = true)]
    public class ConfiguraMapper : BusinessModuloContext
    {
        public override IResultado Execute(IEntrada entry)
        {
            var manager = Support.GetMapperManager();
            manager
                .AddMap<Estoque>(d =>
                {
                    MapperDefault(d);
                    d.IgnoreWrite(p => p.Codigo);
                    d.IgnoreWrite(p => p.NomeTela);
                    d.PropertyParseAndWrite(p => p.Quantidade, "quantidade");
                })
                .AddMap<Produto>(d =>
                {
                    MapperDefault(d);
                    d.PropertyParseAndWrite(p => p.Nome, "nome");
                    d.PropertyParseAndWrite(p => p.Anotacao, "anotacao");
                    d.PropertyParseAndWrite(p => p.Classe, "classe");
                    d.PropertyParseAndWrite(p => p.Preco, "preco");
                    d.PropertyParse(p => p.Categoria, "categoria");
                    d.IgnoreWrite(p => p.CategoriaCodigo);
                    d.ReferenceWrite(p => p.Categoria, "categoria_id");
                })
                .AddMap<CategoriaProduto>(d => {
                    MapperDefault(d);
                    d.PropertyParseAndWrite(p => p.Descricao, "descricao");
                    d.PropertyParseAndWrite(p => p.Anotacao, "anotacao");
                    d.PropertyParse(p => p.CategoriaPai, "categoria_pai");
                    d.ReferenceWrite(p => p.CategoriaPai, "categoria_pai_id");
                })
                .AddMap<Cliente>(d => {
                    MapperDefault(d);
                    d.PropertyParseAndWrite(p => p.Nome, "nome");
                    d.PropertyParseAndWrite(p => p.Apelido, "apelido");
                    d.PropertyParseAndWrite(p => p.Nascimento, "nascimento");
                    d.PropertyParseAndWrite(p => p.Email, "email");
                    d.PropertyParseAndWrite(p => p.Telefone, "telefone");
                    d.PropertyParseAndWrite(p => p.Anotacao, "anotacao");
                    d.PropertyParseAndWrite(p => p.PontuacaoLimite, "pontuacao_limite");
                    d.PropertyParseAndWrite(p => p.PontuacaoAtual, "pontuacao_atual");
                    d.PropertyParseAndWrite(p => p.CreditarLimitePontuacao, "creditar_limite_pontuacao");
                    d.PropertyParseAndWrite(p => p.HistoricoPontos, "historico_pontos");
                    d.PropertyParseAndWrite(p => p.Creditos, "creditos");
                    d.IgnoreWrite(p => p.PontuacaoAdicionalEntrada);
                    d.PropertyParse(p => p.PontuacaoAdicionalEntrada, "pontuacao_entrada");
                })
                .AddMapObject<ClientePonto>(d => {
                    d.PropertyParseAndWrite(p => p.CreatedOn, "created_on");
                    d.PropertyParseAndWrite(p => p.Pontos, "pontos");
                    d.PropertyParseAndWrite(p => p.GerouCredito, "gerou_credito");
                })
                .AddMapObject<ClienteCredito>(d => {
                    d.PropertyParseAndWrite(p => p.CreatedOn, "created_on");
                    d.PropertyParseAndWrite(p => p.Valor, "valor");
                    d.PropertyParseAndWrite(p => p.Usado, "usado");
                })
                .AddMap<Venda>(d => {
                    MapperDefault(d);
                    d.PropertyParseAndWrite(p => p.CreatedOn, "created_on");
                    d.PropertyParseAndWrite(p => p.Itens, "itens");
                    d.PropertyParse(p => p.Cliente, "cliente");
                    d.ReferenceWrite(p => p.Cliente, "cliente_id");
                    d.PropertyParseAndWrite(p => p.Anotacao, "anotacao");
                    d.PropertyParseAndWrite(p => p.ValorTotalCalculado, "valor_total_calculado");
                    d.PropertyParseAndWrite(p => p.ValorTotalSemCalculo, "valor_total_sem_calculo");
                })
                .AddMapObject<ItemVenda>(d => {
                    d.PropertyParseAndWrite(p => p.CreatedOn, "created_on");
                    d.PropertyParseAndWrite(p => p.ValorDeVenda, "valor_venda");
                    d.PropertyParseAndWrite(p => p.Desconto, "desconto");
                    d.PropertyParse(p => p.Produto, "produto");
                    d.ReferenceWrite(p => p.Produto, "produto_id");
                    d.PropertyParseAndWrite(p => p.QuantidadePedida, "quantidade_pedida");
                })
                .AddMapObject<Desconto>(d => {
                    d.PropertyParseAndWrite(p => p.CreatedOn, "created_on");
                    d.PropertyParseAndWrite(p => p.ValorFixo, "valor_fixo");
                    d.PropertyParseAndWrite(p => p.ValorPercentual, "valor_percentual");
                    d.PropertyParseAndWrite(p => p.Classe, "classe");
                })
                ;
            return Support.GetSucessResult(manager);
        }

        private void MapperDefault<T>(IDataMap<T> mapper)
            where T : IDataBis
        {
            mapper.PropertyParse(t => t.Id, "id");
            mapper.PropertyParseAndWrite(t => t.Codigo, "codigo");
            mapper.PropertyParseAndWrite(t => t.CreatedOn, "created_on");
            mapper.PropertyParseAndWrite(t => t.UpdatedOn, "updated_on");
            mapper.PropertyParseAndWrite(t => t.IsDesabilitado, "is_desabilitado");
            //mapper.IgnoreParseAndWrite(t => t.IsErrorValidacao);
            //mapper.IgnoreParseAndWrite(t => t.MessageValidacao);
        }
    }
}