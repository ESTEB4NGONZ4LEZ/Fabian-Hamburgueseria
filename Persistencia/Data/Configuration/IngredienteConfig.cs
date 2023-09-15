
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class IngredienteConfig : IEntityTypeConfiguration<Ingrediente>
{
    public void Configure(EntityTypeBuilder<Ingrediente> builder)
    {
        builder.ToTable("ingrediente");

        builder.Property(x => x.Nombre)
        .HasMaxLength(50)
        .IsRequired();

        builder.Property(x => x.Precio)
        .HasColumnType("double")
        .IsRequired();

        builder.Property(x => x.Stock)
        .HasColumnType("int")
        .IsRequired();
    }
}
