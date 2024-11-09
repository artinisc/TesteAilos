namespace Questao5.Domain.Entities
{
    public class InserirMovimentoDTO
    {
        public string IdMovimento { get; set; }
        public string IdContaCorrente { get; set; }
        public EnumTipoMovimento TipoMovimento { get; set; }
        public decimal Valor { get; set; }
    }
}
