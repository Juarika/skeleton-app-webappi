using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia;

public class SkeletonContext : DbContext
{
    public SkeletonContext(DbContextOptions<SkeletonContext> options) : base(options)
    {
    }

    public DbSet<Pais> Paises { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Ciudad> Ciudades { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<TipoPersona> TiposPersonas { get; set; }
    public DbSet<Salon> Salones { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<TrainerSalon> TrainerSalones { get; set; }
    public DbSet<Genero> Generos { get; set; }
}