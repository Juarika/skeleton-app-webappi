namespace Dominio.Entities;

public class Departamento : BaseEntity
{
    public string Nombre { get; set; }
    public string IdPaisFk { get; set; }
    public Pais Pais { get; set; }
    public ICollection<Ciudad> Ciudades { get; set; }
}