namespace Dominio.Entities;

public class Matricula : BaseEntity
{
    public int IdePersonaFk { get; set; }
    public int IdSalonFk { get; set; }
}