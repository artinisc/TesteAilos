using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database
{
    public interface IRepMovimento
    {
        List<Movimento> Listar();
        void Inserir(Movimento movimento);
    }
}
