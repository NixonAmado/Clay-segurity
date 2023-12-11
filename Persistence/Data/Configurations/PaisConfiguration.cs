
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class PaisConfiguration:IEntityTypeConfiguration<Pais>
{
    public void Configure(EntityTypeBuilder<Pais> builder)
    {
        builder.ToTable("Pais");
        builder.Property(p => p.NombrePais)
        .HasColumnName("Nombre_Pais")
        .IsRequired()
        .HasMaxLength(100);

    }
}