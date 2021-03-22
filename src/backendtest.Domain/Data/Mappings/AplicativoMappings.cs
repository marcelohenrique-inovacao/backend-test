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
            builder.ToTable("Desenvolvedor");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(a => a.DataLancamento)
                .IsRequired()
                .HasColumnType("smalldatetime");

            builder.Property(a => a.Plataforma)
                .IsRequired()
                .HasColumnType("tinyInt");

            builder.HasOne(a => a.Responsavel)
                .WithOne(d => d.ResponsavelAplicativo)
                .OnDelete(DeleteBehavior.NoAction);
                

            builder.HasMany(a => a.Desenvolvedores)
                .WithMany(d => d.Aplicativos);
        }
    }
}