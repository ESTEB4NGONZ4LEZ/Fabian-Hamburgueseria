
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class ChefConfig : IEntityTypeConfiguration<Chef>
{
    public void Configure(EntityTypeBuilder<Chef> builder)
    {
        builder.ToTable("chef");

        builder.Property(x => x.Nombre)
        .HasMaxLength(50)
        .IsRequired();
    }
}
