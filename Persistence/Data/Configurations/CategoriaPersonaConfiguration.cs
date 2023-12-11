
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class CategoriaPersonaConfiguration:IEntityTypeConfiguration<CategoriaPersona>
{
    public void Configure(EntityTypeBuilder<CategoriaPersona> builder)
    {
        builder.ToTable("Categoria_persona");
        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(60);
    }
}