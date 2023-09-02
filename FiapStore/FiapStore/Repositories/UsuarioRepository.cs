using FiapStore.Entities;
using FiapStore.Interfaces;

namespace FiapStore.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private IList<Usuario> _usuarios = new List<Usuario>();

        public IList<Usuario> ObterTodosUsuarios()
        {
           return _usuarios;
        }

        public Usuario ObterUsuarioPorId(int id)
        {
            return _usuarios.FirstOrDefault(usuario => usuario.Id == id);
        }

        public void CadastrarUsuario(Usuario usuario)
        {
            _usuarios.Add(usuario);
        }

        public void AlterarUsuario(Usuario usuario)
        {
            var usuarioParaAlterar = ObterUsuarioPorId(usuario.Id);

            if (usuarioParaAlterar != null)
            {
                usuarioParaAlterar.Nome = usuario.Nome;
            }
        }        

        public void DeletarUsuario(int id)
        {
            _usuarios.Remove(ObterUsuarioPorId(id));
        }

      
    }
}
