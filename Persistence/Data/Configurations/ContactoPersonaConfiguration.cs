
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class ContactoPersonaConfiguration:IEntityTypeConfiguration<ContactoPersona>
{
    public void Configure(EntityTypeBuilder<ContactoPersona> builder)
    {
        builder.ToTable("Contacto_persona");
        builder.Property(p => p.Descripcion)
        .IsRequired()
        .HasMaxLength(60);

        builder.HasOne(p => p.PersonaNavigation)
           .WithMany(p => p.Contactos)
           .HasForeignKey(p => p.Persona_id);

        builder.HasOne(p => p.TContactoNavigation)
           .WithMany(p => p.Contactos)
           .HasForeignKey(p => p.Persona_id);
        
    }
}