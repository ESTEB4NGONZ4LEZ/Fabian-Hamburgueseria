
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class IngredienteRepository : GenericRepository<Ingrediente>, IIngrediente
{
    public IngredienteRepository(DbAppContext context) : base(context)
    {
    }

    public async Task<Ingrediente> GetPan()
    {
        return await _context.Ingredientes
                       .Where(x => x.Nombre.ToLower() == "pan")
                       .FirstOrDefaultAsync();
    }

    public async Task<Ingrediente> GetIngredienteMasCaro()
    {
        return await _context.Ingredientes.OrderByDescending(x => x.Precio)
                                          .FirstAsync();
    }

    public async Task<IEnumerable<Ingrediente>> GetIngredienteRangoPrecio()
    {
        return await _context.Ingredientes
                             .Where(x => x.Precio >= 2 && x.Precio <= 5)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Ingrediente>> GetIngredienteStock()
    {
        return await _context.Ingredientes
                             .Where(x => x.Stock < 400)
                             .ToListAsync();
    }

    public int GetIdPanIntegral()
    {
        return _context.Ingredientes 
                       .Where(x => x.Nombre.ToLower() == "pan integral")
                       .Select(x => x.Id)
                       .First();
    }

    public int GetIdQueso()
    {
        return _context.Ingredientes 
                       .Where(x => x.Nombre.ToLower() == "queso cheddar")
                       .Select(x => x.Id)
                       .First();
    }

    public override async Task
    <(
        int totalRegistros, 
        IEnumerable<Ingrediente> registros
    )> 
    GetAllAsync
    (
        int pageIndex, 
        int pageSize, 
        string search
    )
    {
        var query = _context.Ingredientes as IQueryable<Ingrediente>;
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
