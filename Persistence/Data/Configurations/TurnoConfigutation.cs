
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data;

class TurnoConfiguration:IEntityTypeConfiguration<Turno>
{
    public void Configure(EntityTypeBuilder<Turno> builder)
    {
        builder.ToTable("Turno");
        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(60);

        builder.Property(p => p.HoraTurnoI)
        .HasColumnType("TIME")
        .IsRequired();

        builder.Property(p => p.HoraTurnoF)
        .HasColumnType("TIME")
        .IsRequired();
    }
}