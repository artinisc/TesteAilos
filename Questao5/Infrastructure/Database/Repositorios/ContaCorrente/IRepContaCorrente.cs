using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database
{
    public interface IRepContaCorrente
    {
        List<ContaCorrente> ListarContasCorrentes();
    }
}
