using backendtest.Domain.Domain.Entities;
using backendtest.Domain.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backendtest.Domain.Data.Mappings
{
    public class AplicativoMappings : IEntityTypeConfiguration<Aplicativo>
    { 
        public void Configure(EntityTypeBuilder<Aplicativo> builder)
        {
            builder.ToTable("Aplicativo");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .IsRequired()
                .HasColumnType("VarChar(255)");

            builder.Property(a => a.DataLancamento)
                .IsRequired()
                .HasColumnType("SmallDatetime");

            builder.Property(a => a.Plataforma)
                .IsRequired()
                .HasColumnType("TinyInt");

            builder.HasOne(d => d.Responsavel)
                .WithMany(p => p.Aplicativos)
                .HasForeignKey(d => d.IdDesenvolvedorResponsavel)
                .HasConstraintName("Id_DesenvolvedorResponsavel");

            builder.HasIndex(e => e.IdDesenvolvedorResponsavel, "Idx_Id_DesenvolvedorResponsavel");
            builder.Property(e => e.IdDesenvolvedorResponsavel).HasColumnName("Id_DesenvolvedorResponsavel");
        }
    }
}