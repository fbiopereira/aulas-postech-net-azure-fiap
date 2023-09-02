using FiapStore.Entities;
using FiapStore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FiapStore.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet("obter-todos-usuarios")]
        public IActionResult ObterTodosUsuarios()
        {
            return Ok(_usuarioRepository.ObterTodosUsuarios());
        }

        [HttpGet("obter-usuario-id/{id:int}")] //Opcao especificando o tipo do parametro
        public IActionResult ObterUsuarioId(int id)
        {
            return Ok(_usuarioRepository.ObterUsuarioPorId(id));
        }

        [HttpPost]
        public IActionResult CadastrarUsuario([FromBody] Usuario usuario)
        {
            _usuarioRepository.CadastrarUsuario(usuario);
            return Ok("Usuário cadastrado com sucesso");
        }

        [HttpPut]
        public IActionResult AlterarUsuario([FromBody] Usuario usuario) //A partir do .Net 7 o FromBody é opcional
        {
            _usuarioRepository.AlterarUsuario(usuario);
            return Ok("Usuário alterado com sucesso");
        }

        [HttpDelete("{id}")] //Opcao sem especificar o tipo do parametro
        public IActionResult DeleteUsuario(int id)
        {
            _usuarioRepository.DeletarUsuario(id);
            return Ok("Usuário deletado com sucesso");
        }

    }
}
