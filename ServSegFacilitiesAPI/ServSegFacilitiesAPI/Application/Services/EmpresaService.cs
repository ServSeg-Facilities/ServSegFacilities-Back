using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Identity.Client;
using ServSegFacilitiesAPI.Application.Convertions;
using ServSegFacilitiesAPI.Controllers;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs.EmpresaDTO;
using ServSegFacilitiesAPI.Exceptions;
using ServSegFacilitiesAPI.Interfaces;

namespace ServSegFacilitiesAPI.Application.Services
{
    public class EmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository)
        {
            _empresaRepository = empresaRepository;
        }

        public List<empresa> ListarEmpresas()
        {
            List<empresa> empresas = _empresaRepository.Listar();
            if (empresas == null)
                throw new DomainException("Nenhuma empresa para listar.");

            return empresas;
        }

        public empresa ObterEmpresaPorId(int id)
        {
            empresa empresaRet = _empresaRepository.ObterPorId(id);
            if (empresaRet == null)
                throw new DomainException("Empresa não encontrada.");

            return empresaRet;
        }

        static HttpClient client = new HttpClient();
        public async Task<Uri> CriarEmpresa(string cnpj)
        {
            var response = await client.GetAsync($"https://publica.cnpj.ws/cnpj/{cnpj}");

            response.EnsureSuccessStatusCode();

            var empresaDTO = await response.Content
                .ReadFromJsonAsync<CriarEmpresaDTO>();

            if (empresaDTO == null)
                throw new DomainException("Não foi possível consultar o CNPJ.");

            var empresa = EmpresaParaDTO.converterEmpresaParaDto(empresaDTO);

            _empresaRepository.CriarEmpresa(empresa);

            return response.Headers.Location
                ?? new Uri($"https://publica.cnpj.ws/cnpj/{cnpj}");
        }

        public void AtualizarEmpresa(int id, AtualizarEmpresaDTO empresa)
        {
            if (empresa.nomeFantasia == null || empresa.razaoSocial == null || empresa.cnpj == null || empresa.email == null || empresa.telefone == null || empresa.cep == null)
                throw new DomainException("Campos obrigatórios não preenchidos.");
            _empresaRepository.AtualizarEmpresa(id, EmpresaParaDTO.converterEmpresaAtualizadaDto(empresa));
        }
    }
}

