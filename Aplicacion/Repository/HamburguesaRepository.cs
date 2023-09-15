
using System.Security.Cryptography.X509Certificates;
using System.util.zlib;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class HamburguesaRepository : GenericRepository<Hamburguesa>, IHamburguesa
{
    public HamburguesaRepository(DbAppContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Hamburguesa>> GetHamburguesaInRange()
    {
        return await _context.Hamburguesas
                             .Where(x => x.Precio <= 9)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Hamburguesa>> GetHamburguesaPrecioAscendente()
    {
        return await _context.Hamburguesas
                             .OrderByDescending(x => x.Precio)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Hamburguesa>> GetHamburguesaVegetariana(int idCategoria)
    {
        return await _context.Hamburguesas
                             .Where(x => x.CategoriaId == idCategoria)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Hamburguesa>> GetHamburguesaxChef(int idChef)
    {
        return await _context.Hamburguesas
                             .Where(x => x.ChefId == idChef)
                             .ToListAsync();
    }

    public override async Task
    <(
        int totalRegistros, 
        IEnumerable<Hamburguesa> registros
    )> 
    GetAllAsync
    (
        int pageIndex, 
        int pageSize, 
        string search
    )
    {
        var query = _context.Hamburguesas as IQueryable<Hamburguesa>;
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

    public async Task<Hamburguesa> GetHamburguesaById(int id)
    {
        return await _context.Hamburguesas
                       .Where(x => x.Id == id)
                       .FirstOrDefaultAsync();
    }

    public int GetIdHamburguesaClasica()
    {
        return _context.Hamburguesas
                       .Where(x => x.Nombre.ToLower() == "hamburguesa clasica")
                       .Select(x => x.Id)
                       .First();
    }
}
