using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class PersonaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int IdGeneroFk { get; set; }
    public int IdCiudadFk { get; set; }
    public int IdTipoPerFk { get; set; }
}