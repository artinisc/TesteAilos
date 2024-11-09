using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database
{
    public class IdempotenciaConfig : IEntityTypeConfiguration<Idempotencia>
    {
        public void Configure(EntityTypeBuilder<Idempotencia> builder)
        {
            builder.ToTable("IDEMPOTENCIA");

            builder.HasKey(x => new { x.ChaveIdempotencia });

            builder.Property(x => x.ChaveIdempotencia)
                .HasColumnName("CHAVE_IDEMPOTENCIA")
                .HasMaxLength(37)
                .IsRequired();

            builder.Property(x => x.Requisicao)
                .HasColumnName("REQUISICAO")
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(x => x.Resultado)
                .HasColumnName("RESULTADO")
                .HasMaxLength(1000)
                .IsRequired();
        }
    }
}
