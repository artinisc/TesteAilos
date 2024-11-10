using Questao5.Domain.Entities;

namespace Questao5.Application
{
    public interface IAplicMovimento
    {
        string InserirMovimento(string chaveIdempotencia, InserirMovimentoDTO inserirMovimentoDTO);
    }
}
