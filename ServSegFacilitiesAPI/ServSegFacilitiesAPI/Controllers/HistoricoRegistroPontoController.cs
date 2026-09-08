using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServSegFacilitiesAPI.Application.Services;
using ServSegFacilitiesAPI.DTOs.LogHistoricoRegistroPontoDto;
using ServSegFacilitiesAPI.Exceptions;
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
        public IActionResult Get()
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("usuarioId")?.Value;

            if (!int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return Unauthorized("Usuário inválido no token.");
            }

            var historico = _service.ObterHistoricoListagem(usuarioId);
            return Ok(historico);
        }

        [HttpGet("ObterHistoricoPorId/{historicoId}")]
        [Authorize]
        public ActionResult<ListarLogHistoricoRegistroPontoDto> ObterHistoricoPorId(int historicoId)
        {
            try
            {
                return Ok(_service.ObterHistoricoListagem(historicoId));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message); using Microsoft.AspNetCore.Authorization;
                using Microsoft.AspNetCore.Http;
                using Microsoft.AspNetCore.Mvc;
                using ServSegFacilitiesAPI.Application.Services;
                using ServSegFacilitiesAPI.DTOs.LogHistoricoRegistroPontoDto;
                using ServSegFacilitiesAPI.Exceptions;
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
            public IActionResult Get()
            {
                var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                                  ?? User.FindFirst("usuarioId")?.Value;

                if (!int.TryParse(usuarioIdClaim, out int usuarioId))
                {
                    return Unauthorized("Usuário inválido no token.");
                }

                var historico = _service.ObterHistoricoListagem(usuarioId);
                return Ok(historico);
            }

            [HttpGet("ObterHistoricoPorId/{historicoId}")]
            [Authorize]
            public ActionResult<ListarLogHistoricoRegistroPontoDto> ObterHistoricoPorId(int historicoId)
            {
                try
                {
                    return Ok(_service.ObterHistoricoPorId(historicoId));
                }
                catch (DomainException ex)
                {
                    return NotFound(ex.Message);
                }
            }
        }
    }

}
        }
    }
}
