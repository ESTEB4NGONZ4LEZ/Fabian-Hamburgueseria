
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class HamburguesaIngredienteConfig : IEntityTypeConfiguration<HamburguesaIngrediente>
{
    public void Configure(EntityTypeBuilder<HamburguesaIngrediente> builder)
    {
        builder.ToTable("hamburguesa_ingrediente");

        builder.HasOne(a => a.Hamburguesa)
        .WithMany(e => e.HamburguesaIngredientes)
        .HasForeignKey(i => i.HamburguesaId)
        .IsRequired();

        builder.HasOne(a => a.Ingrediente)
        .WithMany(e => e.HamburguesaIngredientes)
        .HasForeignKey(i => i.IngredienteId)
        .IsRequired();
    }
}
