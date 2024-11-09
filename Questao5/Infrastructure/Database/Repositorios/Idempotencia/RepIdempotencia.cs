namespace Questao5.Infrastructure.Database
{
    public class RepIdempotencia : IRepIdempotencia
    {
        private readonly AppDbContext _context;

        public RepIdempotencia(AppDbContext context)
        {
            _context = context;
        }

    }
}
