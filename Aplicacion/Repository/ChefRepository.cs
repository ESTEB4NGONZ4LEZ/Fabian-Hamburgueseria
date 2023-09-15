
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class ChefRepository : GenericRepository<Chef>, IChef
{
    public ChefRepository(DbAppContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Chef>> GetChefEspecializado()
    {
        return await _context.Chefs
                             .Where(x => x.Especialidad.ToLower() == "carnes")
                             .ToListAsync();
    }

    public int GetIdChef(string nombre)
    {
        return _context.Chefs
                       .Where(x => x.Nombre == nombre)
                       .Select(x => x.Id)
                       .FirstOrDefault();
    }

    public override async Task
    <(
        int totalRegistros, 
        IEnumerable<Chef> registros
    )> 
    GetAllAsync
    (
        int pageIndex, 
        int pageSize, 
        string search
    )
    {
        var query = _context.Chefs as IQueryable<Chef>;
        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(x => x.Nombre.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query.Skip((pageIndex - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();
        return (totalRegistros, registros);
    }
}
