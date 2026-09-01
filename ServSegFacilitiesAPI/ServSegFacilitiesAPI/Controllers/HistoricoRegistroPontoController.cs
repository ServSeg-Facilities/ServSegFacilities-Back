using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServSegFacilitiesAPI.Application.Services;
using System.Security.Claims;

namespace ServSegFacilitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoRegistroPontoController : ControllerBase
    {
        private readonly HistoricoRegistroPontoService _service;
        public HistoricoRegistroPontoController(HistoricoRegistroPontoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("usuarioId")?.Value;

            if (!int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return Unauthorized("Usuário inválido no token.");
            }

            var historico = await _service.ObterHistoricoListagemAsync(usuarioId);
            return Ok(historico);
        }
    }
}
