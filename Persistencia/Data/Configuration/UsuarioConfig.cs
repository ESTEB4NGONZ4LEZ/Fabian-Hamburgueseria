
using System.Security.Cryptography.X509Certificates;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;

public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuario");

        builder.Property(x => x.Username)
        .HasMaxLength(30)
        .IsRequired();

        builder.Property(x => x.Email)
        .HasMaxLength(100)
        .IsRequired();

        builder.Property(x => x.Password)
        .IsRequired();

        builder.HasIndex(x => new {x.Username, x.Email})
        .IsUnique();

        builder.HasMany(x => x.Roles)
        .WithMany(x => x.Usuarios)
        .UsingEntity<UsuarioRoles>
        (
            x => x.HasOne(a => a.Rol)
                  .WithMany(e => e.UsuariosRoles)
                  .HasForeignKey(i => i.RolId),
                  
            x => x.HasOne(a => a.Usuario)
                  .WithMany(e => e.UsuariosRoles)
                  .HasForeignKey(i => i.UsuarioId),
            x => 
            {
                x.HasKey(a => new {a.UsuarioId, a.RolId});
            }
        );
    }
}
