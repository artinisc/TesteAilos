using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Application.Movimento
{
    public class AplicMovimento : IAplicMovimento
    {
        private readonly IRepMovimento _repMovimento;
        private readonly IRepContaCorrente _repContaCorrente;
        private readonly IMapperMovimento _mapperMovimento;

        public AplicMovimento(IRepMovimento repMovimento,
                              IRepContaCorrente repContaCorrente,
                              IMapperMovimento mapperMovimento)
        {
            _repMovimento = repMovimento;
            _repContaCorrente = repContaCorrente;
            _mapperMovimento = mapperMovimento;
        }

        public string InserirMovimento(InserirMovimentoDTO inserirMovimentoDTO)
        {
            ValidarMovimento(inserirMovimentoDTO);

            var movimento = _mapperMovimento.Novo(inserirMovimentoDTO);

            _repMovimento.Inserir(movimento);

            return movimento.IdMovimento;
        }

        public void ValidarMovimento(InserirMovimentoDTO inserirMovimentoDTO)
        {
            var contaCorrente = _repContaCorrente.Recuperar(inserirMovimentoDTO.IdContaCorrente);

            if (contaCorrente == null)
            {
                throw new Exception("");
                // Apenas contas correntes cadastradas podem receber movimentação; TIPO: INVALID_ACCOUNT
            }

            if (contaCorrente.Ativo == EnumAtivo.Inativo)
            {
                // Apenas contas correntes ativas podem receber movimentação; TIPO: INACTIVE_ACCOUNT
            }

            if (inserirMovimentoDTO.Valor <= 0)
            {
                // Apenas valores positivos podem ser recebidos; TIPO: INVALID_VALUE
            }

            if (inserirMovimentoDTO.TipoMovimento != EnumTipoMovimento.Credito && inserirMovimentoDTO.TipoMovimento != EnumTipoMovimento.Debito)
            {
                // Apenas os tipos “débito” ou “crédito” podem ser aceitos; TIPO: INVALID_TYPE
            }
        }
    }
}
