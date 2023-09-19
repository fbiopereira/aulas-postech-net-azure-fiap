using FiapStore.DTOs;
using FiapStore.Entities;
using FiapStore.Enums;
using FiapStore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FiapStore.Controllers
{
    /// <summary>
    /// Manutenção de Usuários do sistema
    /// </summary>
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

        /// <summary>
        /// Retorna uma lista com todos os usuários cadastrados
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        /// </remarks>
        /// <response code="200">Retorna lista de usuários</response>
        /// <response code="401">Usuário não autenticado</response>
        /// <response code="403">Acesso negado</response>
        [Authorize(Roles = Permissoes.Administrador)]
        [HttpGet]
        public IActionResult ObterTodosUsuarios()
        {
            _logger.LogInformation("Listando todos os usuarios");
            
            _logger.LogWarning("Warning listando todos os usuarios");
            try
            {             
                return Ok(_usuarioRepository.ObterTodos());
            }
            catch (Exception ex)
            {                
                _logger.LogError(ex, ex.Message);
            }

            return BadRequest();
            
        }

        /// <summary>
        /// Retorna um usuário específico com a opção de retornar pedidos (desligado por padrão)
        /// </summary>
        /// <param name="id"> Identificador do usuário</param>
        /// <param name="pedidos">Flag para solicitar o retorno dos pedidos associados ao usuário</param>
        /// <returns></returns>
        [Authorize]
        [Authorize(Roles = $"{Permissoes.Administrador}, {Permissoes.Funcionario}")]
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


        [Authorize]
        [Authorize(Roles = Permissoes.Administrador)]
        [HttpPost]
        public IActionResult CadastrarUsuario([FromBody] CadastrarUsuarioDTO usuarioDto)
        {
            
            _usuarioRepository.Cadastrar(new Usuario(usuarioDto));
            return Ok("Usuário cadastrado com sucesso");
        }

        [Authorize]
        [Authorize(Roles = $"{Permissoes.Administrador}, {Permissoes.Funcionario}")]
        [HttpPut]
        public IActionResult AlterarUsuario([FromBody] AlterarUsuarioDTO usuarioDto) //A partir do .Net 7 o FromBody é opcional
        {           
            _usuarioRepository.Alterar(new Usuario(usuarioDto));
            return Ok("Usuário alterado com sucesso");
        }

        [Authorize]
        [Authorize(Roles = Permissoes.Administrador)]
        [HttpDelete("{id}")] //Opcao sem especificar o tipo do parametro
        public IActionResult DeleteUsuario(int id)
        {
            _usuarioRepository.Deletar(id);
            return Ok("Usuário deletado com sucesso");
        }

    }
}
