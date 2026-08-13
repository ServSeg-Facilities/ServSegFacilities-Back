using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ServSegFacilitiesAPI.Application.Services;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs.EmpresaDTO;
using ServSegFacilitiesAPI.Exceptions;
using System.Runtime.CompilerServices;

namespace ServSegFacilitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaService _empresaService;
        public EmpresaController(EmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public IActionResult ListarEmpresas()
        {
            try
            {
                var empresas = _empresaService.ListarEmpresas();
                return Ok(empresas);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet ("EmpresaId/{id}")]
        public IActionResult ObterEmpresaPorId(int id)
        {
            try
            {
                var empresa = _empresaService.ObterEmpresaPorId(id);
                return Ok(empresa);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet ("EmpresaCNPJ/{cnpj}")]
        public IActionResult ObterEmpresaPorCNPJ(string cnpj)
        {
            try
            {
                var empresa = _empresaService.ObterPorCNPJ(cnpj);
                return Ok(empresa);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet ("EmpresaRazaoSocial/{razaoSocial}")]
        public IActionResult ObterEmpresaPorRazaoSocial(string razaoSocial)
        {
            try
            {
                var empresa = _empresaService.ObterPorRazaoSocial(razaoSocial);
                return Ok(empresa);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
            
        [HttpPost ("{cnpj}")]
        public async Task<IActionResult> CriarEmpresa(string cnpj)
        {
            try
            {
                await _empresaService.CriarEmpresa(cnpj);
                return Ok("Empresa criada com sucesso.");
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarEmpresa(int empresaId, AtualizarEmpresaDTO empresaDto)
        {
            try
            {
                _empresaService.AtualizarEmpresa(empresaId, empresaDto);
                return Ok("Empresa atualizada com sucesso.");
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
