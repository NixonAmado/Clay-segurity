
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class PersonaConfiguration:IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("Persona");
        builder.Property(p => p.Id_Persona)
        .HasColumnName("id_Persona")
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(p => p.DateReg)
        .IsRequired()
        .HasColumnType("DATETIME")
        .HasMaxLength(100);

        builder.HasOne(p => p.TPersonaNavigation)
            .WithMany(p => p.Personas)
            .HasForeignKey(p => p.IdTPersona);

        builder.HasOne(p => p.CPersonaNavigation)
            .WithMany(p => p.Personas)
            .HasForeignKey(p => p.CategoriaPersona_id);

        builder.HasOne(p => p.CiudadNavigation)
            .WithMany(p => p.Personas)
            .HasForeignKey(p => p.Ciudad_id);
        
    }
}