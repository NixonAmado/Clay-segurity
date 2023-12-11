
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class DireccionConfiguration:IEntityTypeConfiguration<Direccion>
{
    public void Configure(EntityTypeBuilder<Direccion> builder)
    {
        builder.ToTable("Direccion");
        builder.Property(p => p.Calle)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(p => p.Calle)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(p => p.Carrera)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(p => p.Barrio)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(p => p.Numero)
        .IsRequired();

        builder.HasOne(p => p.PersonaNavigation)
            .WithMany(p => p.Direcciones)
            .HasForeignKey(p => p.TDireccion_id);
    }
}