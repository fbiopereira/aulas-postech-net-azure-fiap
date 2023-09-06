using FiapStore.DTOs;
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

        [HttpGet]
        public IActionResult ObterTodosUsuarios()
        {
            return Ok(_usuarioRepository.ObterTodos());
        }

        [HttpGet("{id:int}/{pedidos:bool=false}")] //Opcao especificando o tipo do parametro
        public IActionResult ObterUsuarioId(int id, bool pedidos = false)
        {
            if (pedidos) 
            {
                return Ok(_usuarioRepository.ObterPorIdComPedidos(id));
            }
            else
            {
                return Ok(_usuarioRepository.ObterPorId(id));
            }
        }

     

        [HttpPost]
        public IActionResult CadastrarUsuario([FromBody] CadastrarUsuarioDTO usuarioDto)
        {
            
            _usuarioRepository.Cadastrar(new Usuario(usuarioDto));
            return Ok("Usuário cadastrado com sucesso");
        }

        [HttpPut]
        public IActionResult AlterarUsuario([FromBody] AlterarUsuarioDTO usuarioDto) //A partir do .Net 7 o FromBody é opcional
        {           
            _usuarioRepository.Alterar(new Usuario(usuarioDto));
            return Ok("Usuário alterado com sucesso");
        }

        [HttpDelete("{id}")] //Opcao sem especificar o tipo do parametro
        public IActionResult DeleteUsuario(int id)
        {
            _usuarioRepository.Deletar(id);
            return Ok("Usuário deletado com sucesso");
        }

    }
}
