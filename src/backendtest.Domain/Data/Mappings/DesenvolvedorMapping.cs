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
             
            builder.OwnsOne(d => d.Cpf, opt =>
            {
                opt.Property(opt => opt.Numero)
                    .IsRequired()
                    .HasMaxLength(CPF.CPFMaxLength)
                    .IsUnicode(false)
                    .HasColumnName("CPF")
                    .HasColumnType("Char")
                    .IsFixedLength();
            });

            builder.OwnsOne(d => d.Email, opt =>
            {
                opt.Property(opt => opt.Endereco)
                    .IsRequired()
                    .HasMaxLength(Email.EnderecoMaxLength)
                    .HasColumnType("VarChar")
                    .HasColumnName("Email")
                    .IsUnicode(false);
            });
        }
    }
}