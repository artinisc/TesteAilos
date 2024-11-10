using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database
{
    public interface IRepIdempotencia
    {
        Idempotencia RecuperarPorChave(string chaveIdempotencia);
        void Salvar(Idempotencia idempotencia);
    }
}
