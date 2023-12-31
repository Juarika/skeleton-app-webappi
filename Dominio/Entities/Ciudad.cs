namespace Dominio.Entities;

public class Ciudad : BaseEntity
{
    public string Nombre { get; set; }
    public int IdDepFk { get; set; }
    public Departamento Departamento { get; set; }
    public ICollection<Persona> Personas { get; set; }
}