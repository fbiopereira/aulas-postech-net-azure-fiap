using FiapStore.Configurations;
using FiapStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FiapStore.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Usuario> Usuario {  get; set; }
        public DbSet<Pedido> Pedido { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                _configuration.GetValue<string>("ConnectionStrings:ConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Uma possibilidade de se configurar o application context com a Fluent API
             * 
            modelBuilder.Entity<Usuario>( usuario =>
            {
                usuario.ToTable("Usuario");
                usuario.HasKey(usuario => usuario.Id);                
                usuario.Property(usuario => usuario.Id)
                .HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
                usuario.Property(usuario => usuario.Nome)
                .HasColumnType("VARCHAR(100)").IsRequired();
                usuario.HasMany(usuario => usuario.Pedidos)
                .WithOne(pedido => pedido.Usuario)
                .HasForeignKey(Pedido => Pedido.Usuario)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Pedido>(pedido =>
            {
                pedido.ToTable("Pedido");
                pedido.HasKey(pedido => pedido.Id);
                pedido.Property(pedido => pedido.Id)
                .HasColumnType("INT").ValueGeneratedNever().UseIdentityColumn();
                pedido.Property(pedido => pedido.NomeProduto).
                HasColumnType("VARCHAR(100)").IsRequired();
                pedido.HasOne(pedido => pedido.Usuario)
                .WithMany(usuario => usuario.Pedidos).HasPrincipalKey(usuario => usuario.Id);
            });

            */

            /* FLUENT API: Refatorando para as configurações estarem em classes de configuração das entidades

            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoConfiguration());

            */


            // FLUENT API:  Pega no código compilado todas as classes que usam aIEntityTypeConfiguration e implementa por padrão
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
