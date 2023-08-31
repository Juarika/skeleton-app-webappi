using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class PaisRepository : GenericRepository<Pais>, IPaisRepository
{
    public PaisRepository(SkeletonContext context) : base(context)
    {

    }
}