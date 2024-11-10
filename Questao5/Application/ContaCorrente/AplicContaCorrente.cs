using FluentAssertions.Equivalency;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Application
{
    public class AplicContaCorrente : IAplicContaCorrente
    {
        private readonly IRepContaCorrente _repContaCorrente;
        private readonly IRepMovimento _repMovimento;

        public AplicContaCorrente(IRepContaCorrente repContaCorrente, 
                                  IRepMovimento repMovimento)
        {
            _repContaCorrente = repContaCorrente;
            _repMovimento = repMovimento;
        }

        public SaldoContaDTO ConsultaSaldo(string idConta)
        {
            var contaCorrente = _repContaCorrente.Recuperar(idConta);

            ValidarConsulta(contaCorrente);

            var saldo = RecuperarSaldo(idConta);

            return new SaldoContaDTO()
            {
                Numero = contaCorrente.Numero,
                Nome = contaCorrente.Nome,
                DataHoraConsulta = DateTime.Now,
                SaldoAtual = saldo
            };
        }

        private void ValidarConsulta(ContaCorrente contaCorrente)
        {
            if (contaCorrente == null)
            {
                throw new ValidacaoDadosException("Apenas contas correntes cadastradas podem consultar o saldo.", "INVALID_ACCOUNT");
            }

            if (contaCorrente.Ativo == EnumAtivo.Inativo)
            {
                throw new ValidacaoDadosException("Apenas contas correntes ativas podem consultar o saldo.", "INACTIVE_ACCOUNT");
            }
        }

        private decimal RecuperarSaldo(string idConta)
        {
            var movimentos = _repMovimento.Listar().Where(x => x.IdContaCorrente.Equals(idConta)).ToList();

            if(movimentos == null)
            {
                return 0;
            }

            var valorCredito = movimentos.Where(x => x.TipoMovimento.Equals('C'))?.Sum(y => y.Valor);
            var valorDebito = movimentos.Where(x => x.TipoMovimento.Equals('D'))?.Sum(y => y.Valor);

            return valorCredito ?? 0 - valorDebito ?? 0;
        }
    }
}
