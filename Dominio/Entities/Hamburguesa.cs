
namespace Dominio.Entities;

public class Hamburguesa : BaseEntity
{
    public string Nombre { get; set; }
    public int CategoriaId { get; set; }
    public double Precio { get; set; }
    public int ChefId { get; set; }
    public Chef Chef { get; set; }
    public Categoria Categoria { get; set; }
    public ICollection<HamburguesaIngrediente> HamburguesaIngredientes { get; set; }
}
