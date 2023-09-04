using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class RegisterDto
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string ApellidoMaterno { get; set; }
    [Required]
    public string ApellidoPaterno { get; set; }
    [Required]
    public int IdGeneroFk { get; set; }
    [Required]
    public int IdCiudadFk { get; set; }
    [Required]
    public int IdTipoPerFk { get; set; }
}