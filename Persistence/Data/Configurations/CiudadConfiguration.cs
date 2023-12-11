
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class CiudadConfiguration:IEntityTypeConfiguration<Ciudad>
{
    public void Configure(EntityTypeBuilder<Ciudad> builder)
    {
        builder.ToTable("Ciudad");
        builder.Property(p => p.NombreCiudad)
        .HasColumnName("Nombre_Ciudad")
        .IsRequired()
        .HasMaxLength(100);

        builder.HasOne(p => p.DepartamentoNavigation)
            .WithMany(p => p.Ciudades)
            .HasForeignKey(p => p.Departamento_id);
    }
}