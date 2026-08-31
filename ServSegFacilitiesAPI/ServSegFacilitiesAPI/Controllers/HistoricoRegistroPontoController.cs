using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServSegFacilitiesAPI.Application.Services;

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
        public async Task<IActionResult> Get()
        {
            try
            {
                var hisorico = await _service.ObterHistoricoListagemAsync();
                return Ok(hisorico);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar historico: " + ex.Message });
            }
        }
    }
}
