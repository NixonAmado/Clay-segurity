
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class TipoContactoConfiguration:IEntityTypeConfiguration<TipoContacto>
{
    public void Configure(EntityTypeBuilder<TipoContacto> builder)
    {
        builder.ToTable("Tipo_contacto");
        builder.Property(p => p.Descripcion)
        .IsRequired()
        .HasMaxLength(60);

    }
}