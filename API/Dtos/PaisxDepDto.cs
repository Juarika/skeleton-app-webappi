namespace API.Dtos;

public class PaisxDepDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<DepartamentoDto> Departamentos { get; set;}
}