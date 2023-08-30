namespace Dominio.Entities;

public class Persona : BaseEntity
{
    public string Nombre { get; set; }
    public int IdGeneroFk { get; set; }
    public int IdCiudadFk { get; set; }
    public int IdTipoPerFk { get; set; }
}