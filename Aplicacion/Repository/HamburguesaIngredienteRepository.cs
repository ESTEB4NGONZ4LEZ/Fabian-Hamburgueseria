
using System.Linq.Expressions;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class HamburguesaIngredienteRepository : IHamburguesaIngrediente
{
    private readonly DbAppContext _context;
    public HamburguesaIngredienteRepository(DbAppContext context)
    {
        _context = context;
    }
    public virtual async Task<IEnumerable<HamburguesaIngrediente>> GetAllAsync()
    {
        return await _context.HamburguesaIngredientes.ToListAsync(); 
    }
    public virtual async Task<HamburguesaIngrediente> GetByIdAsync(int hamburguesaId, int ingredienteId)
    {
        return await _context.HamburguesaIngredientes.FirstOrDefaultAsync(x => x.HamburguesaId == hamburguesaId && x.IngredienteId == ingredienteId);   
    }
    public virtual void Add(HamburguesaIngrediente entity)
    {
        _context.HamburguesaIngredientes.Add(entity);
    }
    public virtual void AddRange(IEnumerable<HamburguesaIngrediente> entities)
    {
        _context.HamburguesaIngredientes.AddRange(entities);
    }
    public virtual void Update(HamburguesaIngrediente entity)
    {
        _context.HamburguesaIngredientes
            .Update(entity);
    }
    public virtual async Task<HamburguesaIngrediente> GetByIdAsync(string id)
    {
       return await _context.HamburguesaIngredientes.FindAsync(id);
    }
    public virtual void Remove(HamburguesaIngrediente entity)
    {
        _context.HamburguesaIngredientes.Remove(entity);
    }
    public virtual void RemoveRange(IEnumerable<HamburguesaIngrediente> entities)
    {
        _context.HamburguesaIngredientes.RemoveRange(entities);
    }
    public virtual IEnumerable<HamburguesaIngrediente> Find(Expression<Func<HamburguesaIngrediente, bool>> expression)
    {
        return _context.HamburguesaIngredientes.Where(expression);
    }
    public virtual async Task<(int totalRegistros, IEnumerable<HamburguesaIngrediente> registros)> GetAllAsync(int pageIndex, int pageSize, string _search)
    {
        var totalRegistros = await _context.HamburguesaIngredientes.CountAsync();
        var registros = await _context.HamburguesaIngredientes
                                      .Skip((pageIndex - 1) * pageSize)
                                      .Take(pageSize)
                                      .ToListAsync();
                                      
        return (totalRegistros, registros);
    }
    public List<int> GetIdHamburguesasPanIntegral(int idIngrediente)
    {
        return _context.HamburguesaIngredientes
                       .Where(x => x.IngredienteId == idIngrediente)
                       .Select(x => x.HamburguesaId)
                       .ToList();
    }

    public List<int> GetIdHamburguesaNoQueso(int idIngrediente)
    {
        return _context.HamburguesaIngredientes
                       .Where(x => x.IngredienteId != idIngrediente)
                       .Select(x => x.HamburguesaId)
                       .ToList();
    }
}
