using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Application
{
    public class AplicIdempotencia : IAplicIdempotencia
    {
        private readonly IRepIdempotencia _repIdempotencia;

        public AplicIdempotencia(IRepIdempotencia repIdempotencia)
        {
            _repIdempotencia = repIdempotencia;
        }

        public RetornoIdempotenciaDTO VerificaIdempotencia(string chaveIdempotencia)
        {
            if (string.IsNullOrEmpty(chaveIdempotencia))
            {
                throw new ValidacaoDadosException("A chave de idempotência é obrigatória.", "INVALID_IDEMPOTENCIA_KEY");
            }

            var idempotenciaExistente = _repIdempotencia.RecuperarPorChave(chaveIdempotencia);

            if (idempotenciaExistente != null)
            {
                return new RetornoIdempotenciaDTO() { Concluido = true, Resultado = idempotenciaExistente.Resultado };
            }
            else
            {
                return new RetornoIdempotenciaDTO() { Concluido = false };
            }
        }

        public void SalvaIdempotenciaMovimento(string chaveIdempotencia, string idMovimento, InserirMovimentoDTO inserirMovimentoDTO)
        {
            var idempotencia = new Idempotencia
            {
                ChaveIdempotencia = chaveIdempotencia,
                Requisicao = $"Codigo do movimento: {inserirMovimentoDTO.IdMovimento}, " +
                             $"codigo da conta corrente: {inserirMovimentoDTO.IdContaCorrente}, " +
                             $"tipo do movimento: {inserirMovimentoDTO.TipoMovimento}, " +
                             $"valor: {inserirMovimentoDTO.Valor}",
                Resultado = idMovimento
            };

            _repIdempotencia.Salvar(idempotencia);
        }
    }
}
