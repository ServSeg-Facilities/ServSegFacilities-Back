using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServSegFacilitiesAPI.Application.Services;
using ServSegFacilitiesAPI.DTOs.RegistroPonto;
using System.Security.Claims;

namespace ServSegFacilitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroPontoController : ControllerBase
    {
        //usa isso pra pegar o usuario logado : var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        private readonly RegistroPontoService _service;
        public RegistroPontoController(RegistroPontoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Adicionar(AdicionarRegistroPonto dto)
        {
            try
            {
                // Pega o ID do usuário logado através do JWT
                var usuarioIdClaim = User.FindFirstValue(
                    ClaimTypes.NameIdentifier
                );

                if (usuarioIdClaim == null)
                {
                    return Unauthorized(
                        "Usuário não identificado."
                    );
                }

                int usuarioId = int.Parse(usuarioIdClaim);

                _service.Adicionar(usuarioId, dto);

                return Ok(
                    "Ponto registrado com sucesso."
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}

