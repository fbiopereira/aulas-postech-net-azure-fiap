using FiapStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FiapStore.Configurations.EntityFramework
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");
            builder.HasKey(pedido => pedido.Id);
            builder.Property(pedido => pedido.Id)
            .HasColumnType("INT").UseIdentityColumn();
            builder.Property(pedido => pedido.NomeProduto).
            HasColumnType("VARCHAR(100)").IsRequired();
            builder.HasOne(pedido => pedido.Usuario)
            .WithMany(usuario => usuario.Pedidos).HasPrincipalKey(usuario => usuario.Id);
        }
    }
}
