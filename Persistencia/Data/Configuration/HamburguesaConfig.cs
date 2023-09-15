
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class HamburguesaConfig : IEntityTypeConfiguration<Hamburguesa>
{
    public void Configure(EntityTypeBuilder<Hamburguesa> builder)
    {
        builder.ToTable("hamburguesa");

        builder.Property(x => x.Nombre)
        .HasMaxLength(50)
        .IsRequired();

        builder.HasOne(a => a.Categoria)
        .WithMany(e => e.Hamburguesas)
        .HasForeignKey(i => i.CategoriaId)
        .IsRequired();

        builder.Property(x => x.Precio)
        .HasColumnType("double")
        .IsRequired();

        builder.HasOne(a => a.Chef)
        .WithMany(e => e.Hamburguesas)
        .HasForeignKey(i => i.ChefId)
        .IsRequired();
    }
}
