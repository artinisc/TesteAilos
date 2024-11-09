namespace Questao5.Infrastructure.Database
{
    public class RepIdempotencia : IRepMovimento
    {
        private readonly AppDbContext _context;

        public RepIdempotencia(AppDbContext context)
        {
            _context = context;
        }

    }
}
