namespace Dominio.Entities;

public class PersonaRol
{
    public int IdPersonaFk { get; set; }
    public Persona Persona { get; set; }
    public int IdRolFk { get; set; }
    public Rol Rol { get; set; }    
}