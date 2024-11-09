using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database
{
    public interface IRepMovimento
    {
        void Inserir(Movimento movimento);
    }
}
