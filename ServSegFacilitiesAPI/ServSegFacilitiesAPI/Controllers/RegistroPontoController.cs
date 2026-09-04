using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServSegFacilitiesAPI.Application.Services;
using ServSegFacilitiesAPI.DTOs.RegistroPonto;
using ServSegFacilitiesAPI.Exceptions;
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
        [HttpGet("StatusAtual")]
        public IActionResult ObterStatusAtual()
        {
            try
            {
                var usuarioIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (usuarioIdClaim == null)
                {
                    return Unauthorized("Usuário não identificado.");
                }

                int usuarioId = int.Parse(usuarioIdClaim);
                var ultimo = _service.BuscarUltimoRegistro(usuarioId);
                var agora = DateTime.Now;

                return Ok(new
                {
                    dataHoraAtual = agora,
                    dataAtual = agora.ToString("dd/MM/yyyy"),
                    horarioAtual = agora.ToString("HH:mm:ss"),
                    diaSemana = agora.ToString("dddd", new System.Globalization.CultureInfo("pt-BR")),
                    ultimoRegistro = ultimo == null ? null : new
                    {
                        ultimo.registroPontoId,
                        ultimo.tipoRegistroId,
                        ultimo.dataHoraPonto,
                        horarioUltimoPonto = ultimo.dataHoraPonto.ToString("HH:mm:ss"),
                        dataUltimoPonto = ultimo.dataHoraPonto.ToString("dd/MM/yyyy"),
                        ultimo.status
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/imagem")]
        public IActionResult ObterImagem(int id)
        {
            try
            {
                var imagem = _service.ObterImagem(id);
                return File(imagem, "image/jpeg");
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Consumes("multipart/form-data")]
        public IActionResult Adicionar([FromForm] AdicionarRegistroPonto dto)
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

                var registro = _service.Adicionar(usuarioId, dto);

                return Ok(new
                {
                    mensagem = "Ponto registrado com sucesso.",
                    registroPontoId = registro.registroPontoId,
                    usuarioId = registro.usuarioId,
                    tipoRegistroId = registro.tipoRegistroId,
                    dataHoraPonto = registro.dataHoraPonto,
                    dataAtual = registro.dataHoraPonto.ToString("dd/MM/yyyy"),
                    horarioAtual = registro.dataHoraPonto.ToString("HH:mm:ss")
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

