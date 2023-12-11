
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class ProgramacionConfiguration:IEntityTypeConfiguration<Programacion>
{
    public void Configure(EntityTypeBuilder<Programacion> builder)
    {
        builder.ToTable("Programacion");
        builder.HasOne(p => p.ContratoNavigation)
        .WithMany(p => p.Programaciones) 
        .HasForeignKey(p => p.Contrato_id);

        builder.HasOne(p => p.TurnoNavigation)
        .WithMany(p => p.Programaciones) 
        .HasForeignKey(p => p.Turno_id);
    
        builder.HasOne(p => p.EmpleadoNavigation)
        .WithMany(p => p.Programaciones) 
        .HasForeignKey(p => p.Empleado_id);
        
    }
}