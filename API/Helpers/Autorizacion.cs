namespace API.Helpers;

public class Autorizacion
{
    public enum Roles 
    {
        Empleado,
        Administrador,
        Gerente
    }

    public const Roles rol_predeterminado = Roles.Empleado;
}