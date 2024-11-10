using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database
{
    public interface IRepContaCorrente
    {
        ContaCorrente Recuperar(string idConta);
    }
}
