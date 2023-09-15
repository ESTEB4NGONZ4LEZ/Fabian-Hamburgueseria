
using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.Repository;

public class IngredienteRepository : GenericRepository<Ingrediente>, IIngrediente
{
    public IngredienteRepository(DbAppContext context) : base(context)
    {
    }
}
