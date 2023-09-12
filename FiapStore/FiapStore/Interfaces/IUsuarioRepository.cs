using FiapStore.Entities;

namespace FiapStore.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        public Usuario ObterPorIdComPedidos(int id);
        public Usuario ObterPorNomeUsuarioESenha(string nomeUsuario, string senha);
    }
}
