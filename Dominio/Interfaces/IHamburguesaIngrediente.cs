
using System.Linq.Expressions;
using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IHamburguesaIngrediente
{
    Task<IEnumerable<HamburguesaIngrediente>> GetAllAsync();
    Task<HamburguesaIngrediente> GetByIdAsync(int hamburguesaId, int ingredienteId);
    void Add(HamburguesaIngrediente entity);
    void AddRange(IEnumerable<HamburguesaIngrediente> entities);
    void Update(HamburguesaIngrediente entity);
    void Remove(HamburguesaIngrediente entity);
    void RemoveRange(IEnumerable<HamburguesaIngrediente> entities);
    IEnumerable<HamburguesaIngrediente> Find(Expression<Func<HamburguesaIngrediente, bool>> expression);
    Task<(int totalRegistros, IEnumerable<HamburguesaIngrediente> registros)> GetAllAsync(int pageIndex, int pageSize, string search);
    List<int> GetIdHamburguesasPanIntegral(int idIngrediente);
    List<int> GetIdHamburguesaNoQueso(int idIngrediente);
}