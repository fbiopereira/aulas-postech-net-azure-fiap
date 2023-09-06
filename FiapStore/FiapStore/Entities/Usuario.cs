using FiapStore.DTOs;

namespace FiapStore.Entities
{
    public class Usuario : Entidade
    {
        public string? Nome { get; set; }
        public ICollection<Pedido>? Pedidos { get; set; }

        public Usuario()
        {            
        }

        public Usuario(CadastrarUsuarioDTO cadastrarUsuarioDTO)
        {
            Nome = cadastrarUsuarioDTO.Nome;
        }

        public Usuario(AlterarUsuarioDTO alterarUsuarioDTO)
        {
            Nome = alterarUsuarioDTO.Nome;
        }
    }
}
