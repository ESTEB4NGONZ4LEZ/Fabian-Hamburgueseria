
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class ChefRepository : GenericRepository<Chef>, IChef
{
    public ChefRepository(DbAppContext context) : base(context)
    {
    }
}
