
using System.Security.Cryptography.X509Certificates;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Persistencia;

namespace Aplicacion.Repository;

public class CategoriaRepository : GenericRepository<Categoria>, ICategoria
{
    public CategoriaRepository(DbAppContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Categoria>> GetCategoriaGormet()
    {
        return await _context.Categorias 
                       .Where(x => x.Descripcion.ToLower().Contains("gourmet"))
                       .ToListAsync();
    }

    public int GetIdCategoria()
    {
        return _context.Categorias
                       .Where(x => x.Nombre.ToLower() == "vegetariana")
                       .Select(x => x.Id)
                       .FirstOrDefault();
    }

    public override async Task
    <(
        int totalRegistros, 
        IEnumerable<Categoria> registros
    )> 
    GetAllAsync
    (
        int pageIndex, 
        int pageSize, 
        string search
    )
    {
        var query = _context.Categorias as IQueryable<Categoria>;
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
