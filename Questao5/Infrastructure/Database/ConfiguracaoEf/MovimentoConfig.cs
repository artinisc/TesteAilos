using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database.ConfiguracaoEf
{
    public class MovimentoConfig : IEntityTypeConfiguration<Movimento>
    {
        public void Configure(EntityTypeBuilder<Movimento> builder)
        {
            builder.ToTable("MOVIMENTO");

            builder.HasKey(x => new { x.IdMovimento });

            builder.Property(x => x.IdMovimento)
                .HasColumnName("IDMOVIMENTO")
                .HasMaxLength(37)
                .IsRequired();

            builder.Property(x => x.IdContaCorrente)
                .HasColumnName("IDCONTACORRENTE")
                .HasMaxLength(37)
                .IsRequired();

            builder.Property(x => x.DataMovimento)
                .HasColumnName("DATAMOVIMENTO")
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(x => x.TipoMovimento)
                .HasColumnName("TIPOMOVIMENTO")
                .HasMaxLength(1)
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasColumnName("VALOR")
                .HasColumnType("decimal(15,2)")
                .IsRequired();
        }
    }
}
