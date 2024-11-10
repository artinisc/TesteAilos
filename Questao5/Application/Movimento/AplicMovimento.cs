using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Application
{
    public class AplicMovimento : IAplicMovimento
    {
        private readonly IRepMovimento _repMovimento;
        private readonly IRepContaCorrente _repContaCorrente;
        private readonly IMapperMovimento _mapperMovimento;
        private readonly IAplicIdempotencia _aplicIdempotencia;

        public AplicMovimento(IRepMovimento repMovimento,
                              IRepContaCorrente repContaCorrente,
                              IMapperMovimento mapperMovimento,
                              IAplicIdempotencia aplicIdempotencia)
        {
            _repMovimento = repMovimento;
            _repContaCorrente = repContaCorrente;
            _mapperMovimento = mapperMovimento;
            _aplicIdempotencia = aplicIdempotencia;
        }

        public string InserirMovimento(string chaveIdempotencia, InserirMovimentoDTO inserirMovimentoDTO)
        {
            ValidarMovimento(inserirMovimentoDTO);

            var movimento = _mapperMovimento.Novo(inserirMovimentoDTO);

            _repMovimento.Inserir(movimento);
            _aplicIdempotencia.SalvaIdempotenciaMovimento(chaveIdempotencia, movimento.IdMovimento, inserirMovimentoDTO);
                
            return movimento.IdMovimento;
        }

        private void ValidarMovimento(InserirMovimentoDTO inserirMovimentoDTO)
        {
            var contaCorrente = _repContaCorrente.Recuperar(inserirMovimentoDTO.IdContaCorrente);

            if (contaCorrente == null)
            {
                throw new ValidacaoDadosException("Apenas contas correntes cadastradas podem receber movimentação.", "INVALID_ACCOUNT");
            }

            if (contaCorrente.Ativo == EnumAtivo.Inativo)
            {
                throw new ValidacaoDadosException("Apenas contas correntes ativas podem receber movimentação.", "INACTIVE_ACCOUNT");
            }

            if (inserirMovimentoDTO.Valor <= 0)
            {
                throw new ValidacaoDadosException("Apenas valores positivos podem ser recebidos.", "INVALID_VALUE");
            }

            if (!inserirMovimentoDTO.TipoMovimento.Equals('C') && !inserirMovimentoDTO.TipoMovimento.Equals('D'))
            {
                throw new ValidacaoDadosException("Apenas os tipos “débito” ou “crédito” podem ser aceitos.", "INVALID_TYPE");
            }
        }
    }
}
