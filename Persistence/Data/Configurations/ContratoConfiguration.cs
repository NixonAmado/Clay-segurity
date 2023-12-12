
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class ContratoConfiguration:IEntityTypeConfiguration<Contrato>
{
    public void Configure(EntityTypeBuilder<Contrato> builder)
    {
        builder.ToTable("Contrato");
        builder.Property(p => p.FechaContrato)
        .IsRequired()
        .HasColumnType("DATETIME");

        builder.Property(p => p.FechaFin)
        .IsRequired()
        .HasColumnType("DATETIME");

        builder.HasOne(p => p.ClienteNavigation)
        .WithMany(p => p.Contratos)
        .HasForeignKey(p => p.Cliente_id);  


        builder.HasOne(p => p.EstadoNavigation)
        .WithMany(p => p.Contratos)
        .HasForeignKey(p => p.Estado_id);    
    }
}