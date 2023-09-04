using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly SkeletonContext context;
    private PaisRepository _paises;
    private PersonaRepository _personas;
    private RolRepository _roles;

    public UnitOfWork(SkeletonContext _context)
    {
        context = _context;
    }
    public IPersonaRepository Personas
    {
        get 
        {
            if (_personas == null)
            {
                _personas = new PersonaRepository(context);
            }
            return _personas;
        }
    }

    public IRolRepository Roles
    {
        get 
        {
            if (_roles == null)
            {
                _roles = new RolRepository(context);
            }
            return _roles;
        }
    }

    public IPaisRepository Paises
    {
        get 
        {
            if (_paises == null)
            {
                _paises = new PaisRepository(context);
            }
            return _paises;
        }
    }

    public int Save()
    {
        return context.SaveChanges();
    }
    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await context.SaveChangesAsync(); 
    }
}