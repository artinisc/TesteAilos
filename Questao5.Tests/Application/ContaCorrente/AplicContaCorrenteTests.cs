using FluentAssertions;
using Moq;
using Questao5.Application;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questao5.Tests.Application
{
    public class AplicContaCorrenteTests
    {
        private readonly Mock<IRepContaCorrente> _mockRepContaCorrente;
        private readonly Mock<IRepMovimento> _mockRepMovimento;
        private readonly AplicContaCorrente _service;

        public AplicContaCorrenteTests()
        {
            _mockRepContaCorrente = new Mock<IRepContaCorrente>();
            _mockRepMovimento = new Mock<IRepMovimento>();
            _service = new AplicContaCorrente(_mockRepContaCorrente.Object, _mockRepMovimento.Object);
        }

        [Fact]
        public void ConsultaSaldo_DeveRetornarSaldoCorreto_QuandoContaValida()
        {
            var idContaCorrente = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9";

            var conta = new ContaCorrente(){
                IdContaCorrente = idContaCorrente,
                Numero = 123, 
                Nome = "Conta Teste", 
                Ativo = EnumAtivo.Ativo
            };

            var movimentos = new List<Movimento>
            {
                new Movimento {
                    //IdMovimento = inserirMovimentoDTO.IdMovimento,
                    IdContaCorrente = idContaCorrente,
                    DataMovimento = DateTime.Now,
                    TipoMovimento = 'C',
                    Valor = 500
                },
                new Movimento {
                    //IdMovimento = inserirMovimentoDTO.IdMovimento,
                    IdContaCorrente = idContaCorrente,
                    DataMovimento = DateTime.Now,
                    TipoMovimento = 'D',
                    Valor = 200
                }
            };

            _mockRepContaCorrente.Setup(r => r.Recuperar(idContaCorrente)).Returns(conta);
            _mockRepMovimento.Setup(r => r.Listar()).Returns(movimentos);

            var saldoDTO = _service.ConsultaSaldo(idContaCorrente);

            saldoDTO.SaldoAtual.Should().Be(300);
        }

        [Fact]
        public void ConsultaSaldo_DeveLancarExcecao_QuandoContaNaoExistir()
        {
            var idContaCorrente = "B9BAFC99 -9999-ED99-A999-999DFA9A99C9";
            _mockRepContaCorrente.Setup(r => r.Recuperar(idContaCorrente)).Returns((ContaCorrente)null);

            Action act = () => _service.ConsultaSaldo(idContaCorrente);

            act.Should().Throw<ValidacaoDadosException>().WithMessage("Apenas contas correntes cadastradas podem consultar o saldo.");
        }

        [Fact]
        public void ConsultaSaldo_DeveLancarExcecao_QuandoContaInativa()
        {
            var idContaCorrente = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9";

            var contaInativa = new ContaCorrente()
            {
                IdContaCorrente = idContaCorrente,
                Numero = 123,
                Nome = "Conta Teste",
                Ativo = EnumAtivo.Inativo
            };

            _mockRepContaCorrente.Setup(r => r.Recuperar(idContaCorrente)).Returns(contaInativa);

            Action act = () => _service.ConsultaSaldo(idContaCorrente);

            act.Should().Throw<ValidacaoDadosException>().WithMessage("Apenas contas correntes ativas podem consultar o saldo.");
        }
    }
}
