using Microsoft.AspNetCore.Mvc;
using ServSegFacilitiesAPI.Application.Services;
using ServSegFacilitiesAPI.DTOs;
using ServSegFacilitiesAPI.DTOs.AutenticacaoDto;
using ServSegFacilitiesAPI.Exceptions;
using System;
using System.Threading.Tasks;

namespace ServSegFacilitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService _service;

        public AutenticacaoController(AutenticacaoService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var token = await _service.Login(loginDto);
                return Ok(token);
            }
            catch (DomainException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao realizar login: " + ex.Message });
            }
        }
    }
}