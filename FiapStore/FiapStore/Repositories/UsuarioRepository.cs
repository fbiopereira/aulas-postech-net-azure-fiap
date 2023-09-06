using Dapper;
using FiapStore.Entities;
using FiapStore.Interfaces;
using System.Data.SqlClient;

namespace FiapStore.Repositories
{
    public class UsuarioRepository : DapperRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Alterar(Usuario entidade)
        {
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                var query = "UPDATE Usuario SET Nome = @Nome WHERE Id = @Id";
                dbConnection.Query(query, entidade);
            }
        }

        public override void Cadastrar(Usuario entidade)
        {
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                var query = "INSERT INTO Usuario (nome) VALUES (@Nome)";
                dbConnection.Execute(query, entidade);
            }
        }

        public override void Deletar(int id)
        {
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                var query = "DELETE FROM Usuario WHERE Id = @Id";
                dbConnection.Execute(query, new { Id = id });
            }
        }

        public override Usuario ObterPorId(int id)
        {
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                var query = "SELECT * FROM Usuario WHERE Id = @Id";
                return dbConnection.Query<Usuario>(query, new { Id = id }).FirstOrDefault();
            }
        }

        public override IList<Usuario> ObterTodos()
        {
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                var query = "SELECT * FROM Usuario";
                return dbConnection.Query<Usuario>(query).ToList();
            }
        }

        public Usuario ObterPorIdComPedidos(int id)
        {
            using (var dbConnection = new SqlConnection(ConnectionString))
            {
                var query = "SELECT Usuario.Id, Usuario.Nome, Pedido.Id, Pedido.NomeProduto FROM Usuario LEFT JOIN Pedido ON Usuario.Id = Pedido.UsuarioId WHERE Usuario.Id = @Id";
                var resultado = new Dictionary<int, Usuario>();
                var parametros = new { Id = id };
                dbConnection.Query<Usuario, Pedido, Usuario>(query,
                    (usuario, pedido) =>
                    {
                        if (!resultado.TryGetValue(usuario.Id, out var usuarioExistente))
                        {
                            usuarioExistente = usuario;
                            usuarioExistente.Pedidos = new List<Pedido>();
                            resultado.Add(usuario.Id, usuarioExistente);
                        }
                        if (pedido != null)
                        {
                            usuarioExistente.Pedidos.Add(pedido);
                        }

                        return usuarioExistente;

                    }, parametros, splitOn: "Id");

                var usuario = resultado.Values.FirstOrDefault();

                return usuario;

            }
        }
    }
}
