
namespace API.Dtos;

public class HamburguesaDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int CategoriaId { get; set; }
    public double Precio { get; set; }
    public int ChefId { get; set; }
}
