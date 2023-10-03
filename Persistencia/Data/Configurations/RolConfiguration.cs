using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;

public class RolConfiguration : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        // Aquí puedes configurar las propiedades de la entidad Marca
        // utilizando el objeto 'builder'.
        builder.ToTable("rol");

        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(50);
        
        builder
        .HasMany(p => p.Personas)
        .WithMany(p => p.Roles)
        .UsingEntity<PersonaRoles>(
            j => j
                .HasOne(pt => pt.Persona)
                .WithMany(p => p.PersonaRoles)
                .HasForeignKey(pt => pt.IdPersonaFk),
            j => j
                .HasOne(pt => pt.Rol)
                .WithMany(t => t.PersonaRoles)
                .HasForeignKey(pt => pt.IdRolFk),
            j => 
            {
                j.HasKey(pt => new { pt.IdPersonaFk, pt.IdRolFk });
            }
        );    
    }
}