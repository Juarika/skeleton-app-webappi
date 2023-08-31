using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        // Aquí puedes configurar las propiedades de la entidad Marca
        // utilizando el objeto 'builder'.
        builder.ToTable("matricula");

        builder.HasOne(t => t.Salon)
        .WithMany(s => s.Matriculas)
        .HasForeignKey(p => p.IdSalonFk);

        builder.HasOne(p => p.Persona)
        .WithMany(c => c.Matriculas)
        .HasForeignKey(p => p.IdPersonaFk);
    }
}