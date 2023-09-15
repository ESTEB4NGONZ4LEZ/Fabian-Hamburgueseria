

namespace Dominio.Interfaces;

public interface IUnitOfWork
{
    ICategoria Categoria { get; }
    IChef Chef { get; }
    IHamburguesa Hamburguesa { get; }
    IHamburguesaIngrediente HamburguesaIngrediente { get; }
    IIngrediente Ingrediente { get;}
    IUsuario Usuarios {get;}
    IRol Roles {get;}
    Task<int> SaveAsync();
}

