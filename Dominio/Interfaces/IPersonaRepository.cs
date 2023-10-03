using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IPersonaRepository : IGenericRepository<Persona>
{
    Task<Persona> GetByUsernameAsync(string username);
}