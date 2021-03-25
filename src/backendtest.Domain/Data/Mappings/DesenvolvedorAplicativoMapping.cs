using backendtest.Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backendtest.Domain.Data.Mappings
{
    public class DesenvolvedorAplicativoMapping : IEntityTypeConfiguration<DesenvolvedorAplicativo>
    {
        public void Configure(EntityTypeBuilder<DesenvolvedorAplicativo> builder)
        {  
            builder.ToTable("DesenvolvedorAplicativo");

                builder.HasIndex(e => e.FkAplicativo, "Idx_Id_Aplicativo");
 
                builder.HasIndex(e => e.FkDesenvolvedor, "Idx_Id_Desenvolvedor");

                builder.HasIndex(e => new { e.FkDesenvolvedor, e.FkAplicativo }, "Idx_Id_Desenvolvedor_Id_Aplicativo");

                builder.Property(e => e.FkAplicativo).HasColumnName("Id_Aplicativo");

                builder.Property(e => e.FkDesenvolvedor).HasColumnName("Id_Desenvolvedor");

                builder.HasOne(d => d.FkAplicativoNavigation)
                    .WithMany(p => p.desenvolvedorAplicativo)
                    .HasForeignKey(d => d.FkAplicativo)
                    .HasConstraintName("Id_Aplicativo");

                builder.HasOne(d => d.FkDesenvolvedorNavigation)
                    .WithMany(p => p.desenvolvedorAplicativo)
                    .HasForeignKey(d => d.FkDesenvolvedor)
                    .HasConstraintName("Id_Desenvolvedor");
        }
    }
}