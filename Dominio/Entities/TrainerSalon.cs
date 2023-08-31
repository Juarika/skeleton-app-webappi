namespace Dominio.Entities;

public class TrainerSalon
{
    public int IdTrainerFk { get; set; }
    public Persona Persona { get; set; }
    public int IdSalonFk { get; set;}
    public Salon Salon { get; set; }
}