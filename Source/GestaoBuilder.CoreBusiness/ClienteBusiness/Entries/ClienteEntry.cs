namespace Modular.CoreBusiness.ClienteBusiness.Entries
{
    public enum ClienteAcao
    {
        AddPontuacao = 1
    }

    public class ClienteEntry
    {
        public string ClienteCodigo { get; set; }
        public int Pontuacao { get; set; }
        public ClienteAcao Acao { get; set; }
    }
}
