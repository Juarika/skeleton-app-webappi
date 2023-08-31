using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class TrainerSalonConfiguration : IEntityTypeConfiguration<TrainerSalon>
{
    public void Configure(EntityTypeBuilder<TrainerSalon> builder)
    {
        // Aquí puedes configurar las propiedades de la entidad Marca
        // utilizando el objeto 'builder'.
        builder.ToTable("trainersalon");

        builder.HasOne(t => t.Salon)
        .WithMany(s => s.TrainerSalones)
        .HasForeignKey(p => p.IdSalonFk);

        builder.HasOne(p => p.Persona)
        .WithMany(c => c.TrainerSalones)
        .HasForeignKey(p => p.IdTrainerFk);
    }
}