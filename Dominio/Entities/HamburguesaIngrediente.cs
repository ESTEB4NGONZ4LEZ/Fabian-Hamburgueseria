
namespace Dominio.Entities;

public class HamburguesaIngrediente
{
    public int HamburguesaId { get; set; }
    public int IngredienteId { get; set; }
    public Hamburguesa Hamburguesa { get; set; }
    public Ingrediente Ingrediente { get; set; }
}
