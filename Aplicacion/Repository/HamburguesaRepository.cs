
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class HamburguesaRepository : GenericRepository<Hamburguesa>, IHamburguesa
{
    public HamburguesaRepository(DbAppContext context) : base(context)
    {
    }
}
