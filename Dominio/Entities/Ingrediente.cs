
namespace Dominio.Entities;

public class Ingrediente : BaseEntity
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public double Precio { get; set; }
    public int Stock { get; set; }
    public ICollection<HamburguesaIngrediente> HamburguesaIngredientes { get; set; }

} 
