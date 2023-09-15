
using Dominio.Entities;

namespace Dominio.Interfaces;

public interface ICategoria : IGenericRepository<Categoria>
{
    Task<IEnumerable<Categoria>> GetCategoriaGormet();
    int GetIdCategoria(); 
}
