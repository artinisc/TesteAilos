using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database
{
    public class RepIdempotencia : IRepIdempotencia
    {
        private readonly AppDbContext _context;

        public RepIdempotencia(AppDbContext context)
        {
            _context = context;
        }

        public Idempotencia RecuperarPorChave(string chaveIdempotencia)
        {
            return _context.Idempotencia.Where(x => x.ChaveIdempotencia.Equals(chaveIdempotencia)).FirstOrDefault();
        }

        public void Salvar(Idempotencia idempotencia)
        {
            _context.Idempotencia.Add(idempotencia);
            _context.SaveChanges();
        }
    }
}
