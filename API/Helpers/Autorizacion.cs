namespace API.Helpers;

public class Autorizacion
{
    public Enum Roles 
    {
        Administrador,
        Gerente,
        Empleado
    }

    public const Roles rol_predeterminado = Roles.Empleado;
}