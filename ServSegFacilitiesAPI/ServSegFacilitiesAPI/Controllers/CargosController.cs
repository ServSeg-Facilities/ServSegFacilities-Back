using System;
using System.Threading.Tasks;
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
    public class CargosController : ControllerBase
    {
        private readonly CargoService _cargoService;

        public CargosController(CargoService cargoService)
        {
            _cargoService = cargoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cargos = await _cargoService.ListarTodos();
                return Ok(cargos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar cargos: " + ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var cargo = await _cargoService.BuscarPorId(id);
                if (cargo == null)
                    return NotFound(new { mensagem = "Cargo não encontrado." });

                return Ok(cargo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao buscar cargo: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CargoCriarDto dto)
        {
            try
            {
                await _cargoService.Cadastrar(dto);
                return StatusCode(201, new { mensagem = "Cargo cadastrado com sucesso!" });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao cadastrar cargo: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CargoCriarDto dto)
        {
            try
            {
                await _cargoService.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao atualizar cargo: " + ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _cargoService.Deletar(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno ao remover cargo: " + ex.Message });
            }
        }
    }
}