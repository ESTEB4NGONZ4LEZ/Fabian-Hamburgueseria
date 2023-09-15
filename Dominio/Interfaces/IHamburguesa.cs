
using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IHamburguesa : IGenericRepository<Hamburguesa>
{
    Task<IEnumerable<Hamburguesa>> GetHamburguesaVegetariana(int idCategoria); 
    Task<IEnumerable<Hamburguesa>> GetHamburguesaInRange();
    Task<IEnumerable<Hamburguesa>> GetHamburguesaPrecioAscendente();
    Task<IEnumerable<Hamburguesa>> GetHamburguesaxChef(int idCategoria);
    Task<Hamburguesa> GetHamburguesaById(int id);  
    int GetIdHamburguesaClasica();
}
