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

        public ContaCorrente Recuperar(string idConta)
        {
            return _context.ContaCorrente.Where(x => x.IdContaCorrente.Equals(idConta)).FirstOrDefault();
        }
    }
}
