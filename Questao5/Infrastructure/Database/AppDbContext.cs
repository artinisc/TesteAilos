using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Database.ConfiguracaoEf;

namespace Questao5.Infrastructure.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ContaCorrente> ContaCorrente { get; set; }
        public DbSet<Movimento> Movimento { get; set; }
        public DbSet<Idempotencia> Idempotencia { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ContaCorrenteConfig());
            modelBuilder.ApplyConfiguration(new MovimentoConfig());
            modelBuilder.ApplyConfiguration(new IdempotenciaConfig());
        }
    }
}