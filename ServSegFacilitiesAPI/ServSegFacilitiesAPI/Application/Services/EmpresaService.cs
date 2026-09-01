using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Identity.Client;
using ServSegFacilitiesAPI.Application.Convertions;
using ServSegFacilitiesAPI.Controllers;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs.EmpresaDTO;
using ServSegFacilitiesAPI.DTOs.LocalizacaoEmpresaDTO;
using ServSegFacilitiesAPI.Exceptions;
using ServSegFacilitiesAPI.Interfaces;
using System.Net;
using System.Runtime.ConstrainedExecution;

namespace ServSegFacilitiesAPI.Application.Services
{
    public class EmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly ILocalizacaoEmpresaRepository _localizacaoEmpresaRepository;

        private static readonly HttpClient _httpClient = CriarHttpClient();

        private static HttpClient CriarHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept", "application/json, text/html, */*");
            client.DefaultRequestHeaders.Add("Accept-Language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");
            return client;
        }

        public EmpresaService(IEmpresaRepository empresaRepository, ILocalizacaoEmpresaRepository localizacaoEmpresaRepository)
        {
            _empresaRepository = empresaRepository;
            _localizacaoEmpresaRepository = localizacaoEmpresaRepository;
        }

        public List<ListarEmpresaDTO> ListarEmpresas()
        {
            var empresas = _empresaRepository.Listar().Select(emp => new ListarEmpresaDTO
            {
                empresaId = emp.empresaId,
                cnpj = emp.cnpj,
                razaoSocial = emp.razaoSocial,
                nomeFantasia = emp.nomeFantasia,
                telefone = emp.telefone,
                email = emp.email,
                cep = emp.cep,
                logradouro = emp.logradouro,
                numero = emp.numero,
                complemento = emp.complemento,
                bairro = emp.bairro,
                cidade = emp.cidade,
                estado = emp.estado
            }).ToList();

            return empresas;
        }

        public empresa ObterEmpresaPorId(int id)
        {
            empresa empresaRet = _empresaRepository.ObterPorId(id);
            if (empresaRet == null)
                throw new DomainException("Empresa não encontrada.");

            return empresaRet;
        }

        public empresa ObterPorCNPJ(string cnpj)
        {
            empresa empresaRet = _empresaRepository.ObterPorCNPJ(cnpj);
            if (empresaRet == null)
                throw new DomainException("Empresa não encontrada.");

            return empresaRet;
        }

        public empresa ObterPorRazaoSocial(string razaoSocial)
        {
            empresa empresaRet = _empresaRepository.ObterPorRazaoSocial(razaoSocial);
            if (empresaRet == null)
                throw new DomainException("Empresa não encontrada.");

            return empresaRet;
        }

        public async Task<Uri> CriarEmpresa(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                throw new DomainException("CNPJ é obrigatório.");

            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Trim();
            if (_empresaRepository.ObterPorCNPJ(cnpj) != null)
                throw new DomainException("Empresa já cadastrada");

            var response = await _httpClient.GetAsync($"https://brasilapi.com.br/api/cnpj/v1/{cnpj}");
            if (!response.IsSuccessStatusCode)
                throw new DomainException("Não foi possível consultar o CNPJ na Brasil API.");

            var empresaDTO = await response.Content.ReadFromJsonAsync<CriarEmpresaDTO>();
            if (empresaDTO == null)
                throw new DomainException("Não foi possível processar os dados do CNPJ.");

            empresa empresa = EmpresaParaDTO.converterEmpresaParaDto(empresaDTO);

            var cep = empresa.cep;
            if (cep == null)
                throw new DomainException("CEP não encontrado no cadastro do CNPJ.");
            cep = cep.Replace("-", "").Replace(".", "").Trim();

            empresa.cnpj = cnpj;
            empresa.nomeFantasia = string.IsNullOrWhiteSpace(empresa.nomeFantasia) ? empresa.razaoSocial : empresa.nomeFantasia;
            if (string.IsNullOrWhiteSpace(empresa.email))
            {
                empresa.email = empresa.razaoSocial.ToLower().Replace(" ", "").Replace(".", "") + "@email.com";
            }

            _empresaRepository.CriarEmpresa(empresa);

            try
            {
                var responseLat = await _httpClient.GetAsync($"https://brasilapi.com.br/api/cep/v2/{cep}");
                if (responseLat.IsSuccessStatusCode)
                {
                    CriarLocalizacaoEmpresaDTO? locDto = await responseLat.Content.ReadFromJsonAsync<CriarLocalizacaoEmpresaDTO>();
                    if (locDto?.Location?.Coordinates != null &&
                        !string.IsNullOrWhiteSpace(locDto.Location.Coordinates.Latitude) &&
                        !string.IsNullOrWhiteSpace(locDto.Location.Coordinates.Longitude))
                    {
                        _localizacaoEmpresaRepository.AdicionarLocalizacaoEmpresa(new localizacaoEmpresa
                        {
                            empresaId = empresa.empresaId,
                            latitude = locDto.Location.Coordinates.Latitude,
                            longitude = locDto.Location.Coordinates.Longitude,
                            precisao = 100,
                        });
                    }
                }
            }
            catch
            {
                // Falha ao obter geolocalização do CEP não impede o cadastro da empresa
            }

            return new Uri($"https://brasilapi.com.br/api/cnpj/v1/{cnpj}");
        }

        public void AtualizarEmpresa(int id, AtualizarEmpresaDTO empresa)
        {
            if (empresa.nomeFantasia == null || empresa.razaoSocial == null || empresa.cnpj == null || empresa.email == null || empresa.telefone == null || empresa.cep == null)
                throw new DomainException("Campos obrigatórios não preenchidos.");
            _empresaRepository.AtualizarEmpresa(id, EmpresaParaDTO.converterEmpresaAtualizadaDto(empresa));
        }
    }
}

