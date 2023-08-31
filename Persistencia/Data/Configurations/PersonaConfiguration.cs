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
    }
}