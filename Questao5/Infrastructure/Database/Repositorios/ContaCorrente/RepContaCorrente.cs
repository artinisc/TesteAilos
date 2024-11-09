using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database
{
    public class RepContaCorrente : IRepContaCorrente
    {
        private readonly AppDbContext _context;

        public RepContaCorrente(AppDbContext context)
        {
            _context = context;
        }

        public List<ContaCorrente> ListarContasCorrentes()
        {
            return _context.ContaCorrente.ToList();
        }
    }
}
