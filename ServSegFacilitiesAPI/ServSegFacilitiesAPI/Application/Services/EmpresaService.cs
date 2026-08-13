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
using System.Runtime.ConstrainedExecution;

namespace ServSegFacilitiesAPI.Application.Services
{
    public class EmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly ILocalizacaoEmpresaRepository _localizacaoEmpresaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository, ILocalizacaoEmpresaRepository localizacaoEmpresaRepository)
        {
            _empresaRepository = empresaRepository;
            _localizacaoEmpresaRepository = localizacaoEmpresaRepository;
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

        static HttpClient client = new HttpClient();
        public async Task<Uri> CriarEmpresa(string cnpj)
        {
            // Essas três linhas são para fazer a api pensar que a request é do navegador.
            // Assim a Brasil API não da block na request.
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");
            client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8");
            client.DefaultRequestHeaders.Add("Accept-Language", "pt-BR,pt;q=0.9,en-US;q=0.8,en;q=0.7");

            cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");

            // Execulta o end point.
            var response = await client.GetAsync($"https://brasilapi.com.br/api/cnpj/v1/{cnpj}");
            var empresaDTO = await response.Content.ReadFromJsonAsync<CriarEmpresaDTO>();
            if (empresaDTO == null)
                throw new DomainException("Não foi possível consultar o CNPJ.");

            empresa empresa = EmpresaParaDTO.converterEmpresaParaDto(empresaDTO);

            var cep = empresa.cep.Replace("-", "").Replace(".", "").Trim();

            try
            {

                var responseLat = await client.GetAsync($"https://brasilapi.com.br/api/cep/v2/{cep}");
                bool cepValido = responseLat.IsSuccessStatusCode;
                CriarLocalizacaoEmpresaDTO locDto = await responseLat.Content.ReadFromJsonAsync<CriarLocalizacaoEmpresaDTO>();
                Console.WriteLine($"CEP: {cep} \n{cepValido}");
                Console.WriteLine($"Latitude: {locDto.Location.Coordinates.Latitude} \n Longitude: {locDto.Location.Coordinates.Latitude}");

                _localizacaoEmpresaRepository.AdicionarLocalizacaoEmpresa(_empresaRepository.ObterPorCNPJ(empresa.cnpj).empresaId, new localizacaoEmpresa
                {
                    latitude = locDto.Location.Coordinates.Latitude,
                    longitude = locDto.Location.Coordinates.Longitude,
                    precisao = 100,
                });

            }
            catch (Exception ex)
            {

                Console.Write(ex.ToString());
            }


            empresa.cnpj = cnpj;
            empresa.nomeFantasia = String.IsNullOrWhiteSpace(empresa.nomeFantasia) ? empresa.razaoSocial : empresa.nomeFantasia;
            if (empresa.email == null)
            {
                empresa.email = empresa.razaoSocial.ToLower().Replace(" ", "").Replace(".", "");
                empresa.email += "@email.com";
            }

            _empresaRepository.CriarEmpresa(empresa);
            return response.Headers.Location ?? new Uri($"https://brasiilapi.com.br/cnpj/v1/{cnpj}");
        }

        public void AtualizarEmpresa(int id, AtualizarEmpresaDTO empresa)
        {
            if (empresa.nomeFantasia == null || empresa.razaoSocial == null || empresa.cnpj == null || empresa.email == null || empresa.telefone == null || empresa.cep == null)
                throw new DomainException("Campos obrigatórios não preenchidos.");
            _empresaRepository.AtualizarEmpresa(id, EmpresaParaDTO.converterEmpresaAtualizadaDto(empresa));
        }
    }
}

