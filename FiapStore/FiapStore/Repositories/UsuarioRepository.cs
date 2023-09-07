using Dapper;
using FiapStore.Entities;
using FiapStore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace FiapStore.Repositories
{
    public class UsuarioRepository : EFRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Usuario ObterPorIdComPedidos(int id)
        {
            return _context.Usuario
                .Include(usuario => usuario.Pedidos)
                .Where(usuario => usuario.Id == id)
                .ToList()
                .Select(usuario =>
                {
                    usuario.Pedidos = usuario.Pedidos.Select(pedido => new Pedido(pedido)).ToList();
                    return usuario;
                }).FirstOrDefault();

        }
    }
}
