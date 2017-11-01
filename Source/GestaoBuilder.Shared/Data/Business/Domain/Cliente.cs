using System;
using System.Collections.Generic;
using Modular.Shared.Atributos;

namespace Modular.Shared.Data.Business.Domain
{
    [Data(DocumentName = "clientes")]
    public class Cliente : DataModificada<Cliente>, IDataBis
    {
        public string Codigo { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDesabilitado { get; set; }
        public string Id { get; set; }

        public string Nome { get; set; }
        public string Apelido { get; set; }
        public DateTime Nascimento { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Anotacao { get; set; }

        /// <summary>
        /// Pontuação a ser adicionada
        /// </summary>
        public decimal PontuacaoAdicionalEntrada {
            get;
            set;
        }

        /// <summary>
        /// Indica a quantidade de pontos atuais
        /// </summary>
        public decimal PontuacaoAtual
        {
            get => Getter(c => c.PontuacaoAtual);
            set => Setter(c => c.PontuacaoAtual, value);
        }

        /// <summary>
        /// Indica a pontuação limite
        /// </summary>
        public decimal PontuacaoLimite { get; set; }
        /// <summary>
        /// Parametro indica quanto deve creditar ao chegar na pontuação limite
        /// </summary>
        public decimal CreditarLimitePontuacao { get; set; }

        /// <summary>
        /// Histórico de pontos
        /// </summary>
        public ClientePonto[] HistoricoPontos
        {
            get => Getter(c => c.HistoricoPontos);
            set => Setter(c => c.HistoricoPontos, value);
        }

        public void AddHistoricoPontos(ClientePonto ponto)
        {
            ponto.CreatedOn = DateTime.Now;
            var historico = new List<ClientePonto>(HistoricoPontos ?? new ClientePonto[] { }) {ponto};
            HistoricoPontos = historico.ToArray();
        }

        /// <summary>
        /// Creditos
        /// </summary>
        public ClienteCredito[] Creditos {
            get => Getter(c => c.Creditos);
            set => Setter(c => c.Creditos, value);
        }

        public void AddCredito(ClienteCredito credito)
        {
            credito.CreatedOn = CreatedOn;
            var creditos = new List<ClienteCredito>(Creditos ?? new ClienteCredito[] { }) { credito };
            Creditos = creditos.ToArray();
        }
    }
}