using FluentAssertions;
using Moq;
using Questao5.Application;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Tests.Application
{
    public class AplicIdempotenciaTests
    {
        private readonly Mock<IRepIdempotencia> _repIdempotenciaMock;
        private readonly AplicIdempotencia _aplicIdempotencia;

        public AplicIdempotenciaTests()
        {
            _repIdempotenciaMock = new Mock<IRepIdempotencia>();
            _aplicIdempotencia = new AplicIdempotencia(_repIdempotenciaMock.Object);
        }

        [Fact]
        public void VerificaIdempotencia_DeveLancarExcecao_QuandoChaveIdempotenciaForNulaOuVazia()
        {
            string chaveIdempotenciaNula = null;
            string chaveIdempotenciaVazia = "";

            Action actNula = () => _aplicIdempotencia.VerificaIdempotencia(chaveIdempotenciaNula);
            Action actVazia = () => _aplicIdempotencia.VerificaIdempotencia(chaveIdempotenciaVazia);

            actNula.Should().Throw<ValidacaoDadosException>()
                .WithMessage("A chave de idempotência é obrigatória.");
            actVazia.Should().Throw<ValidacaoDadosException>()
                .WithMessage("A chave de idempotência é obrigatória.");
        }

        [Fact]
        public void VerificaIdempotencia_DeveRetornarResultado_QuandoChaveIdempotenciaExistir()
        {
            var chaveIdempotencia = "chave-existente";
            var idempotenciaExistente = new Idempotencia
            {
                ChaveIdempotencia = chaveIdempotencia,
                Resultado = "mov-123"
            };

            _repIdempotenciaMock.Setup(x => x.RecuperarPorChave(chaveIdempotencia))
                .Returns(idempotenciaExistente);

            var resultado = _aplicIdempotencia.VerificaIdempotencia(chaveIdempotencia);

            resultado.Concluido.Should().BeTrue();
            resultado.Resultado.Should().Be("mov-123");
        }

        [Fact]
        public void VerificaIdempotencia_DeveRetornarConcluidoFalse_QuandoChaveIdempotenciaNaoExistir()
        {
            var chaveIdempotencia = "chave-nao-existente";

            _repIdempotenciaMock.Setup(x => x.RecuperarPorChave(chaveIdempotencia))
                .Returns((Idempotencia)null);

            var resultado = _aplicIdempotencia.VerificaIdempotencia(chaveIdempotencia);

            resultado.Concluido.Should().BeFalse();
            resultado.Resultado.Should().BeNull();
        }

        [Fact]
        public void SalvaIdempotenciaMovimento_DeveSalvarIdempotencia_QuandoDadosForemValidos()
        {
            var chaveIdempotencia = "chave-idempotente";
            var idMovimento = "mov-123";
            var inserirMovimentoDTO = new InserirMovimentoDTO
            {
                IdMovimento = idMovimento,
                IdContaCorrente = "123",
                TipoMovimento = 'C',
                Valor = 100
            };

            _aplicIdempotencia.SalvaIdempotenciaMovimento(chaveIdempotencia, idMovimento, inserirMovimentoDTO);

            _repIdempotenciaMock.Verify(x => x.Salvar(It.Is<Idempotencia>(i =>
                i.ChaveIdempotencia == chaveIdempotencia &&
                i.Requisicao.Contains("Codigo do movimento: mov-123") &&
                i.Resultado == idMovimento
            )), Times.Once);
        }
    }
}
