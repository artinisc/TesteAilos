using Questao5.Domain.Entities;

namespace Questao5.Application
{
    public interface IAplicIdempotencia
    {
        RetornoIdempotenciaDTO VerificaIdempotencia(string chaveIdempotencia);
        void SalvaIdempotenciaMovimento(string chaveIdempotencia, string idMovimento, InserirMovimentoDTO inserirMovimentoDTO);
    }
}
