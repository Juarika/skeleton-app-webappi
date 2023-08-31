namespace Dominio.Entities;

public class Salon : BaseEntity
{
    public string Nombre { get; set; }
    public int Capacidad { get; set; }
    public ICollection<Matricula> Matriculas { get; set; }
    public ICollection<TrainerSalon> TrainerSalones { get; set; }
}