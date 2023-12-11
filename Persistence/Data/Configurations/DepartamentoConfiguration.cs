
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class DepartamentoConfiguration:IEntityTypeConfiguration<Departamento>
{
    public void Configure(EntityTypeBuilder<Departamento> builder)
    {
        builder.ToTable("Departamento");
        builder.Property(p => p.NombreDep)
        .HasColumnName("Nombre_Departamento")
        .IsRequired()
        .HasMaxLength(100);

        builder.HasOne(p => p.PaisNavigation)
            .WithMany(p => p.Departamentos)
            .HasForeignKey(p => p.Pais_id);
    }
}