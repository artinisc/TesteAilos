using System.Drawing;

namespace Questao5.Domain.Entities
{
    public class MapperMovimento : IMapperMovimento
    {
        public Movimento Novo(InserirMovimentoDTO inserirMovimentoDTO)
        {
            return new Movimento()
            {
                IdMovimento = inserirMovimentoDTO.IdMovimento,
                IdContaCorrente = inserirMovimentoDTO.IdContaCorrente,
                DataMovimento = DateTime.Now,
                TipoMovimento = inserirMovimentoDTO.TipoMovimento,
                Valor = inserirMovimentoDTO.Valor
            };
        }
    }
}
