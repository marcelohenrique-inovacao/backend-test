using backendtest.Domain.Domain.Entities;
using backendtest.Domain.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backendtest.Domain.Data.Mappings
{
    public class DesenvolvedorMapping : IEntityTypeConfiguration<Desenvolvedor>
    {
        public void Configure(EntityTypeBuilder<Desenvolvedor> builder)
        {
            builder.ToTable("Desenvolvedor");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Nome)
                .IsRequired()
                .HasColumnType("VarChar(255)");

            builder.OwnsOne(c => c.Cpf, tf =>
            {
                tf.Property(c => c.Numero)
                    .IsRequired()
                    .HasMaxLength(CPF.CPFMaxLength)
                    .HasColumnName("Cpf")
                    .HasColumnType($"Char({CPF.CPFMaxLength})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Endereco)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType($"VarChar({Email.EnderecoMaxLength})");
            });

            builder.HasMany(d => d.Aplicativos)
                .WithMany(a => a.Desenvolvedores);
        }
    }
}