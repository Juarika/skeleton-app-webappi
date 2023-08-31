using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;

public class SalonConfiguration : IEntityTypeConfiguration<Salon>
{
    public void Configure(EntityTypeBuilder<Salon> builder)
    {
        // Aquí puedes configurar las propiedades de la entidad Marca
        // utilizando el objeto 'builder'.
        builder.ToTable("salon");

        builder.Property(s => s.Nombre)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(s => s.Capacidad)
       .HasColumnType("int");

       builder
        .HasMany(p => p.Personas)
        .WithMany(p => p.Salones)
        .UsingEntity<TrainerSalon>(
            j => j
                .HasOne(pt => pt.Persona)
                .WithMany(t => t.TrainerSalones)
                .HasForeignKey(pt => pt.IdTrainerFk),
            j => j
                .HasOne(pt => pt.Salon)
                .WithMany(p => p.TrainerSalones)
                .HasForeignKey(pt => pt.IdSalonFk),
            j => 
            {
                j.HasKey(pt => new { pt.IdTrainerFk, pt.IdSalonFk });
            }
        );
    }
}