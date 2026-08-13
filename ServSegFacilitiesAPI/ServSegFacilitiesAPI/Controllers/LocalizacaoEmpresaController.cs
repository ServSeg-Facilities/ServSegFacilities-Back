using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServSegFacilitiesAPI.Application.Services;
using ServSegFacilitiesAPI.DTOs.LocalizacaoEmpresaDTO;

namespace ServSegFacilitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoEmpresaController : ControllerBase
    {
        private readonly LocalizacaoEmpresaService _localizacaoEmpresaService;
        public LocalizacaoEmpresaController(LocalizacaoEmpresaService localizacaoEmpresaService)
        {
            _localizacaoEmpresaService = localizacaoEmpresaService;
        }

        [HttpGet("EmpresaId/{id}")]
        public ActionResult<ListarLocalizacaoEmpresaDTO> ObterPorLocalizacaoEmpresaId(int id)
        {
            try
            {
                return Ok(_localizacaoEmpresaService.ObterPorEmpresaId(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<CriarLocalizacaoEmpresaDTO> AdicionarLocalizacaoEmpresa(CriarLocalizacaoEmpresaDTO locDto)
        {
            try
            {
                _localizacaoEmpresaService.AdicionarLocalizacaoEmpresa(locDto);
                return Ok(locDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("AtualizarLocalizacaoEmpresa{empresaId}")]
        public IActionResult AtualizarLocalizacaoEmpresa(int empresaId, AtualizarLocalizacaoEmpresaDTO locDto)
        {
            try
            {
                _localizacaoEmpresaService.AtualizarLocalizacaoEmpresa(empresaId, locDto);
                return Ok(locDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
