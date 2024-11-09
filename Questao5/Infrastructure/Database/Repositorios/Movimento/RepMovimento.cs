namespace Questao5.Infrastructure.Database
{
    public class RepMovimento : IRepMovimento
    {
        private readonly AppDbContext _context;

        public RepMovimento(AppDbContext context)
        {
            _context = context;
        }

    }
}
