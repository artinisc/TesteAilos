namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        public string IdContaCorrente { get; set; }
        public int Numero { get; set; }
        public string Nome { get; set; }
        public EnumAtivo Ativo { get; set; }

        public List<Movimento>? Movimentos { get; set; }
    }

    public enum EnumAtivo
    {
        Inativo = 0,
        Ativo = 1
    }
}
