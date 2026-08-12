using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServSegFacilitiesAPI.Application.Services;
using ServSegFacilitiesAPI.DTOs;
using ServSegFacilitiesAPI.Exceptions;

namespace ServSegFacilitiesAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuarios = await _usuarioService.ListarTodos();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar usuários: " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioService.BuscarPorId(id);
                if (usuario == null)
                    return NotFound(new { mensagem = "Usuário não encontrado." });

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar usuário: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioCriarDto dto)
        {
            try
            {
                await _usuarioService.Cadastrar(dto);
                return StatusCode(201, new { mensagem = "Usuário cadastrado com sucesso!" });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao cadastrar usuário: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UsuarioCriarDto dto)
        {
            try
            {
                await _usuarioService.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao atualizar usuário: " + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _usuarioService.Deletar(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao remover usuário: " + ex.Message });
            }
        }
    }
}