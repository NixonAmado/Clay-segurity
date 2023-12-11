
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class TipoDireccionConfiguration:IEntityTypeConfiguration<TipoDireccion>
{
    public void Configure(EntityTypeBuilder<TipoDireccion> builder)
    {
        builder.ToTable("Tipo_direccion");
        builder.Property(p => p.Descripcion)
        .IsRequired()
        .HasMaxLength(60);

    }
}