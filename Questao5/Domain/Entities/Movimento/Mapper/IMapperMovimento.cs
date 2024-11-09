namespace Questao5.Domain.Entities
{
    public interface IMapperMovimento
    {
        Movimento Novo(InserirMovimentoDTO inserirMovimentoDTO);
    }
}
