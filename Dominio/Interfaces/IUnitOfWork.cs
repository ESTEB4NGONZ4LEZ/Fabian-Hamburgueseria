

namespace Dominio.Interfaces;

public interface IUnitOfWork
{
    ICategoria Categoria { get; }
    IChef Chef { get; }
    IHamburguesa Hamburguesa { get; }
    IHamburguesaIngrediente HamburguesaIngrediente { get; }
    IIngrediente Ingrediente { get;}
    Task<int> SaveAsync();
}

