using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    private readonly SkeletonContext _context;
    public RolRepository(SkeletonContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Rol>> GetAllAsync()
    {
        return await _context.Roles
                    .Include(p => p.Personas)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Rol> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Roles as IQueryable<Rol>;
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Nombre.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query
                                .Include(p => p.Personas)
                                .Skip((pageIndex - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();
        return (totalRegistros, registros);
    }
}