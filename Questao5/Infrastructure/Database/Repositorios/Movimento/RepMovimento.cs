using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database
{
    public class RepMovimento : IRepMovimento
    {
        private readonly AppDbContext _context;

        public RepMovimento(AppDbContext context)
        {
            _context = context;
        }

        public void Inserir(Movimento movimento)
        {
            _context.Movimento.Add(movimento);
            _context.SaveChanges();
        }
    }
}
