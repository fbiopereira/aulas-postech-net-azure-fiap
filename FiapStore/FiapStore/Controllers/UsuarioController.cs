using FiapStore.DTOs;
using FiapStore.Entities;
using FiapStore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FiapStore.Controllers
{
    [ApiController]
    [Route("Usuario")]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioRepository usuarioRepository, ILogger<UsuarioController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;

        }

        [HttpGet]
        public IActionResult ObterTodosUsuarios()
        {
            _logger.LogInformation("Listando todos os usuarios");
            
            _logger.LogWarning("Warning listando todos os usuarios");
            try
            {
               throw new Exception("Deu Erro");
                return Ok(_usuarioRepository.ObterTodos());
            }
            catch (Exception ex)
            {                
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest();
            
        }

        [HttpGet("{id:int}/{pedidos:bool=false}")] //Opcao especificando o tipo do parametro
        public IActionResult ObterUsuarioId(int id, bool pedidos = false)
        {
            _logger.LogInformation("Executando ObterUsuarioId com id={id} e pedidos={pedidos}", id, pedidos);
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
