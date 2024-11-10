using Questao5.Domain.Entities;

namespace Questao5.Application
{
    public interface IAplicContaCorrente
    {
        SaldoContaDTO ConsultaSaldo(string idConta);
    }
}
