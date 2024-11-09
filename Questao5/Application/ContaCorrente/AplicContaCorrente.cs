using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database;

namespace Questao5.Application
{
    public class AplicContaCorrente : IAplicContaCorrente
    {
        private readonly IRepContaCorrente _repContaCorrente;

        public AplicContaCorrente(IRepContaCorrente repContaCorrente)
        {
            _repContaCorrente = repContaCorrente;
        }

        public List<ContaCorrente> ListarContasCorrentes()
        {
            return _repContaCorrente.ListarContasCorrentes();
        }
    }
}
