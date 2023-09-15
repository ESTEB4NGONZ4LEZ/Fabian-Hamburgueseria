
using Dominio.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dominio.Interfaces;

public interface IIngrediente : IGenericRepository<Ingrediente>
{
    Task<IEnumerable<Ingrediente>> GetIngredienteStock(); 
    Task<Ingrediente> GetIngredienteMasCaro(); 
    Task<IEnumerable<Ingrediente>> GetIngredienteRangoPrecio(); 
    Task<Ingrediente> GetPan();
    int GetIdPanIntegral();
    int GetIdQueso();
}
