using FluentAssertions;
using Moq;
using Questao5.Application;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Tests.Application
{
    public class AplicMovimentoTests
    {
        private readonly Mock<IRepMovimento> _repMovimentoMock;
        private readonly Mock<IRepContaCorrente> _repContaCorrenteMock;
        private readonly Mock<IMapperMovimento> _mapperMovimentoMock;
        private readonly Mock<IAplicIdempotencia> _aplicIdempotenciaMock;
        private readonly AplicMovimento _aplicMovimento;

        public AplicMovimentoTests()
        {
            _repMovimentoMock = new Mock<IRepMovimento>();
            _repContaCorrenteMock = new Mock<IRepContaCorrente>();
            _mapperMovimentoMock = new Mock<IMapperMovimento>();
            _aplicIdempotenciaMock = new Mock<IAplicIdempotencia>();

            _aplicMovimento = new AplicMovimento(
                _repMovimentoMock.Object,
                _repContaCorrenteMock.Object,
                _mapperMovimentoMock.Object,
                _aplicIdempotenciaMock.Object
            );
        }

        [Fact]
        public void InserirMovimento_DeveLancarExcecao_QuandoContaNaoExistir()
        {
            var movimentoDTO = new InserirMovimentoDTO
            {
                IdContaCorrente = "B9BAFC99 -9999-ED99-A999-999DFA9A99C9",
                TipoMovimento = 'C',
                Valor = 100
            };

            _repContaCorrenteMock.Setup(x => x.Recuperar(It.IsAny<string>())).Returns((ContaCorrente)null);

            Action act = () => _aplicMovimento.InserirMovimento("chave-idempotente", movimentoDTO);

            act.Should().Throw<ValidacaoDadosException>()
                .WithMessage("Apenas contas correntes cadastradas podem receber movimentação.");
        }

        [Fact]
        public void InserirMovimento_DeveLancarExcecao_QuandoContaEstiverInativa()
        {
            var idContaCorrente = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9";

            var contaInativa = new ContaCorrente()
            {
                IdContaCorrente = idContaCorrente,
                Numero = 123,
                Nome = "Conta Teste",
                Ativo = EnumAtivo.Inativo
            };


            var movimentoDTO = new InserirMovimentoDTO
            {
                IdContaCorrente = idContaCorrente,
                TipoMovimento = 'C',
                Valor = 100
            };

            _repContaCorrenteMock.Setup(x => x.Recuperar(It.IsAny<string>())).Returns(contaInativa);

            Action act = () => _aplicMovimento.InserirMovimento("chave-idempotente", movimentoDTO);

            act.Should().Throw<ValidacaoDadosException>()
                .WithMessage("Apenas contas correntes ativas podem receber movimentação.");
        }

        [Fact]
        public void InserirMovimento_DeveLancarExcecao_QuandoValorForInvalido()
        {
            var idContaCorrente = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9";

            var conta = new ContaCorrente()
            {
                IdContaCorrente = idContaCorrente,
                Numero = 123,
                Nome = "Conta Teste",
                Ativo = EnumAtivo.Ativo
            };

            var movimentoDTO = new InserirMovimentoDTO
            {
                IdContaCorrente = idContaCorrente,
                TipoMovimento = 'C',
                Valor = -50
            };

            _repContaCorrenteMock.Setup(x => x.Recuperar(It.IsAny<string>())).Returns(conta);

            Action act = () => _aplicMovimento.InserirMovimento("chave-idempotente", movimentoDTO);

            act.Should().Throw<ValidacaoDadosException>()
                .WithMessage("Apenas valores positivos podem ser recebidos.");
        }

        [Fact]
        public void InserirMovimento_DeveLancarExcecao_QuandoTipoMovimentoForInvalido()
        {
            var idContaCorrente = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9";

            var conta = new ContaCorrente()
            {
                IdContaCorrente = idContaCorrente,
                Numero = 123,
                Nome = "Conta Teste",
                Ativo = EnumAtivo.Ativo
            };

            var movimentoDTO = new InserirMovimentoDTO
            {
                IdContaCorrente = idContaCorrente,
                TipoMovimento = 'X',
                Valor = 100
            };

            _repContaCorrenteMock.Setup(x => x.Recuperar(It.IsAny<string>())).Returns(conta);

            Action act = () => _aplicMovimento.InserirMovimento("chave-idempotente", movimentoDTO);

            act.Should().Throw<ValidacaoDadosException>()
                .WithMessage("Apenas os tipos “débito” ou “crédito” podem ser aceitos.");
        }

        [Fact]
        public void InserirMovimento_DeveRetornarIdMovimento_QuandoDadosForemValidos()
        {
            var idContaCorrente = "B6BAFC09 -6967-ED11-A567-055DFA4A16C9";

            var conta = new ContaCorrente()
            {
                IdContaCorrente = idContaCorrente,
                Numero = 123,
                Nome = "Conta Teste",
                Ativo = EnumAtivo.Ativo
            };

            var movimentoDTO = new InserirMovimentoDTO
            {
                IdContaCorrente = idContaCorrente,
                TipoMovimento = 'C',
                Valor = 100
            };

            var movimento = new Movimento { IdMovimento = "mov-123" };

            _repContaCorrenteMock.Setup(x => x.Recuperar(It.IsAny<string>())).Returns(conta);
            _mapperMovimentoMock.Setup(x => x.Novo(It.IsAny<InserirMovimentoDTO>())).Returns(movimento);
            _repMovimentoMock.Setup(x => x.Inserir(It.IsAny<Movimento>())).Verifiable();
            _aplicIdempotenciaMock.Setup(x => x.SalvaIdempotenciaMovimento(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<InserirMovimentoDTO>())).Verifiable();

            var result = _aplicMovimento.InserirMovimento("chave-idempotente", movimentoDTO);

            result.Should().Be(movimento.IdMovimento);
            _repMovimentoMock.Verify(x => x.Inserir(It.IsAny<Movimento>()), Times.Once);
            _aplicIdempotenciaMock.Verify(x => x.SalvaIdempotenciaMovimento(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<InserirMovimentoDTO>()), Times.Once);
        }
    }
}
