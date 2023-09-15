
using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IChef : IGenericRepository<Chef>
{  
    Task<IEnumerable<Chef>> GetChefEspecializado(); 
    int GetIdChef(string nombre);
}
