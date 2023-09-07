using FiapStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapStore.Configurations.EntityFramework
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(usuario => usuario.Id);
            builder.Property(usuario => usuario.Id)
            .HasColumnType("INT").UseIdentityColumn();
            builder.Property(usuario => usuario.Nome)
            .HasColumnType("VARCHAR(100)").IsRequired();
            builder.HasMany(usuario => usuario.Pedidos)
            .WithOne(pedido => pedido.Usuario)
            .HasForeignKey(Pedido => Pedido.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
