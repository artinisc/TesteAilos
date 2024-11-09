using Questao5.Domain.Entities;

namespace Questao5.Application
{
    public interface IAplicMovimento
    {
        string InserirMovimento(InserirMovimentoDTO inserirMovimentoDTO);
    }
}
