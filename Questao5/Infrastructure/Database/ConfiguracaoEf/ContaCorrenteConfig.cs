using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Database
{
    public class ContaCorrenteConfig : IEntityTypeConfiguration<ContaCorrente>
    {
        public void Configure(EntityTypeBuilder<ContaCorrente> builder)
        {
            builder.ToTable("CONTACORRENTE");

            builder.HasKey(x => new { x.IdContaCorrente });

            builder.Property(x => x.IdContaCorrente)
                .HasColumnName("IDCONTACORRENTE")
                .HasMaxLength(37)
                .IsRequired();

            builder.Property(x => x.Numero)
                .HasColumnName("NUMERO")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Ativo)
                .HasColumnName("ATIVO")
                .HasMaxLength(1)
                .IsRequired();

            builder.HasMany(x => x.Movimentos)
                .WithOne()
                .HasForeignKey(x => x.IdContaCorrente)
                .IsRequired(false);
        }
    }
}
