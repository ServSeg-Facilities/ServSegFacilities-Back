using ServSegFacilitiesAPI.Application.Convertions;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs.LocalizacaoEmpresaDTO;
using ServSegFacilitiesAPI.Exceptions;
using ServSegFacilitiesAPI.Interfaces;

namespace ServSegFacilitiesAPI.Application.Services
{
    public class LocalizacaoEmpresaService
    {
        private readonly ILocalizacaoEmpresaRepository _localizacaoRepo;
        public LocalizacaoEmpresaService(ILocalizacaoEmpresaRepository localizacaoRepo)
        {
            _localizacaoRepo = localizacaoRepo;
        }

        public ListarLocalizacaoEmpresaDTO ObterPorEmpresaId(int empresaId)
        {
            localizacaoEmpresa? local = _localizacaoRepo.ObterPorEmpresaId(empresaId);
            if (local == null)
                throw new DomainException("Localização da empresa não encontrada.");

            return new ListarLocalizacaoEmpresaDTO
            {
                localizacaoEmpresaId = local.localizacaoEmpresaId,
                empresaId = local.empresaId,
                latitude = local.latitude,
                longitude = local.longitude,
                precisao = local.precisao
            };
        }

        public void AdicionarLocalizacaoEmpresa(CriarLocalizacaoEmpresaDTO localizacaoEmpresa)
        {
            if (localizacaoEmpresa?.Location?.Coordinates == null ||
                string.IsNullOrWhiteSpace(localizacaoEmpresa.Location.Coordinates.Latitude) ||
                string.IsNullOrWhiteSpace(localizacaoEmpresa.Location.Coordinates.Longitude))
            {
                throw new DomainException("Localização não cadastrada! Preencha todos os campos.");
            }

            if (localizacaoEmpresa.empresaId <= 0)
            {
                throw new DomainException("Identificador da empresa inválido.");
            }

            _localizacaoRepo.AdicionarLocalizacaoEmpresa(LocalizacaoEmpresaParaDTO.ConverterLocalizacaoParaDto(localizacaoEmpresa));
        }

        public void AtualizarLocalizacaoEmpresa(int empresaId, AtualizarLocalizacaoEmpresaDTO localizacaoEmpresa)
        {
            if (string.IsNullOrWhiteSpace(localizacaoEmpresa.latitude) ||
                string.IsNullOrWhiteSpace(localizacaoEmpresa.longitude) ||
                localizacaoEmpresa.precisao == null)
            {
                throw new DomainException("Localização não atualizada! Preencha todos os campos.");
            }

            _localizacaoRepo.AtualizarLocalizacaoEmpresa(empresaId, LocalizacaoEmpresaParaDTO.convertAtualizarLocalizacaoParaDto(localizacaoEmpresa));
        }
    }
}
