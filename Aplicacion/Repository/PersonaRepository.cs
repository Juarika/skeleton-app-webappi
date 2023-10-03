using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class PersonaRepository : GenericRepository<Persona>, IPersonaRepository
{
    private readonly SkeletonContext _context;
    public PersonaRepository(SkeletonContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Persona>> GetAllAsync()
    {
        return await _context.Personas
                    .Include(p => p.Roles)
                    .Include(p => p.Matriculas)
                    .Include(p => p.Salones)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Persona> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Personas as IQueryable<Persona>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Roles)
                                .Include(p => p.Matriculas)
                                .Include(p => p.Salones)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return (totalRegistros, registros);
    }

    public async Task<Persona> GetByUsernameAsync(string username)
    {
        return await _context.Personas
                            .Include(u=>u.Roles)
                            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }
}