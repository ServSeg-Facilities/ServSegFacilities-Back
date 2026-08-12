using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServSegFacilitiesAPI.Application.Services;
using ServSegFacilitiesAPI.DTOs.TipoRegistroDto;
using ServSegFacilitiesAPI.Exceptions;

namespace ServSegFacilitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoRegistroController : ControllerBase
    {
        private readonly TipoRegistroService _service;
        public TipoRegistroController(TipoRegistroService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<List<ListarTipoRegistroDto>> Listar()
        {
            List<ListarTipoRegistroDto> lista = _service.Listar();
            return Ok(lista);
        }
        [HttpGet("{id}")]
        public ActionResult<ListarTipoRegistroDto> BuscarPorId(int id)
        {
            try
            {
                ListarTipoRegistroDto area = _service.BuscarPorId(id);
                return Ok(area);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult Adicionar(AdicionarTipoRegistroDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public ActionResult Atualizar(int id, AtualizarTipoRegistroDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
