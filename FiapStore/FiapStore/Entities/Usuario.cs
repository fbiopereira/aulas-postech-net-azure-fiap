using FiapStore.DTOs;
using FiapStore.Enums;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

namespace FiapStore.Entities
{
    
    // DATA ANNOTATION [Table("Usuario")]
    public class Usuario : Entidade
    {
        // DATA ANNOTATION [Required]
        // DATA ANNOTATION [Column("Nome")]
        public string Nome { get; set; }
        public  string NomeUsuario { get; set; }
        public  string Senha { get; set; }
        public  TipoPermissao Permissao { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }

        public Usuario()
        {            
        }

        public Usuario(CadastrarUsuarioDTO cadastrarUsuarioDTO)
        {
            Nome = cadastrarUsuarioDTO.Nome;
            NomeUsuario = cadastrarUsuarioDTO.NomeUsuario;
            Senha = cadastrarUsuarioDTO.Senha;
            Permissao = cadastrarUsuarioDTO.Permissao;
        }

        public Usuario(AlterarUsuarioDTO alterarUsuarioDTO)
        {
            Id = alterarUsuarioDTO.Id;
            Nome = alterarUsuarioDTO.Nome;
        }
    }
}
