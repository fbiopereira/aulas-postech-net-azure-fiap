using FiapStore.DTOs;
using FiapStore.Interfaces;
using FiapStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace FiapStore.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public LoginController(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Autenticar([FromBody] LoginDTO loginDTO)
        {
            var usuario = _usuarioRepository.ObterPorNomeUsuarioESenha(loginDTO.NomeUsuario, loginDTO.Senha);

            if (usuario == null)
            {
                return NotFound(new {mensagem = "Usuário ou Senha inválidos!!!"});
            }

            var token = _tokenService.GerarToken(usuario);
            
            usuario.Senha = null;

            return Ok(new
            {
                Usuario = usuario,
                Token = token
            });
        }
    }
}
