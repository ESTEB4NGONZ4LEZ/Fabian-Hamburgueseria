

using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DbAppContext _context;
    public UnitOfWork(DbAppContext context)
    {
        _context = context;
    } 

    RolRepository _rol;
    UsuarioRepository _usuario;
    CategoriaRepository _categoria;
    ChefRepository _chef;
    HamburguesaIngredienteRepository _hamburguesaIngrediente;
    HamburguesaRepository _hamburguesa;
    IngredienteRepository _ingrediente;
    
    public IUsuario Usuarios
    {
        get
        {
            if (_usuario is not null)
            {
                return _usuario;
            }
            return _usuario = new UsuarioRepository(_context);
        }
    }
    public IRol Roles
    {
        get
        {
            if (_rol is not null)
            {
                return _rol;
            }
            return _rol = new RolRepository(_context);
        }
    }

    public ICategoria Categoria
    {
        get
        {
            if (_categoria is not null)
            {
                return _categoria;
            }
            return _categoria = new CategoriaRepository(_context);
        }
    }

    public IChef Chef
    {
        get
        {
            if (_chef is not null)
            {
                return _chef;
            }
            return _chef = new ChefRepository(_context);
        }
    }

    public IHamburguesa Hamburguesa
    {
        get
        {
            if (_hamburguesa is not null)
            {
                return _hamburguesa;
            }
            return _hamburguesa = new HamburguesaRepository(_context);
        }
    }

    public IHamburguesaIngrediente HamburguesaIngrediente
    {
        get
        {
            if (_hamburguesaIngrediente is not null)
            {
                return _hamburguesaIngrediente;
            }
            return _hamburguesaIngrediente = new HamburguesaIngredienteRepository(_context);
        }
    }

    public IIngrediente Ingrediente
    {
        get
        {
            if (_ingrediente is not null)
            {
                return _ingrediente;
            }
            return _ingrediente = new IngredienteRepository(_context);
        }
    }
    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

}