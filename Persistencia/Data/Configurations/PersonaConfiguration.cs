using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        // Aquí puedes configurar las propiedades de la entidad Marca
        // utilizando el objeto 'builder'.
        builder.ToTable("persona");

        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(50);

        builder.HasOne(p => p.Ciudad)
        .WithMany(c => c.Personas)
        .HasForeignKey(p => p.IdCiudadFk);

        builder.HasOne(p => p.Genero)
        .WithMany(c => c.Personas)
        .HasForeignKey(p => p.IdGeneroFk);

        builder.HasOne(p => p.TipoPersona)
        .WithMany(c => c.Personas)
        .HasForeignKey(p => p.IdTipoPerFk);

        builder
        .HasMany(p => p.Salones)
        .WithMany(p => p.Personas)
        .UsingEntity<TrainerSalon>(
            j => j
                .HasOne(pt => pt.Salon)
                .WithMany(t => t.TrainerSalones)
                .HasForeignKey(pt => pt.IdSalonFk),
            j => j
                .HasOne(pt => pt.Persona)
                .WithMany(p => p.TrainerSalones)
                .HasForeignKey(pt => pt.IdTrainerFk),
            j => 
            {
                j.HasKey(pt => new { pt.IdTrainerFk, pt.IdSalonFk });
            }
        );

        builder
        .HasMany(p => p.Roles)
        .WithMany(p => p.Personas)
        .UsingEntity<PersonaRoles>(
            j => j
                .HasOne(pt => pt.Rol)
                .WithMany(t => t.PersonaRoles)
                .HasForeignKey(pt => pt.IdRolFk),
            j => j
                .HasOne(pt => pt.Persona)
                .WithMany(p => p.PersonaRoles)
                .HasForeignKey(pt => pt.IdPersonaFk),
            j => 
            {
                j.HasKey(pt => new { pt.IdPersonaFk, pt.IdRolFk });
            }
        );
    }
}