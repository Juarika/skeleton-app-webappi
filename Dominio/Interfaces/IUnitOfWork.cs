namespace Dominio.Interfaces;

public interface IUnitOfWork
{
    IPaisRepository Paises { get; }    
}