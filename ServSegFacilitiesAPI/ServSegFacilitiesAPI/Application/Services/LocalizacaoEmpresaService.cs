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
            localizacaoEmpresa local = _localizacaoRepo.ObterPorLocalizacaoEmpresaId(empresaId);
            if (local == null)
                throw new DomainException("localizacao da empresa não encontrada");

            return new ListarLocalizacaoEmpresaDTO
            {
                localizacaoEmpresaId = local.localizacaoEmpresaId,
                empresaId = empresaId,
                latitude = local.latitude,
                longitude = local.longitude,
                precisao = local.precisao
            };
        }

        public void AdicionarLocalizacaoEmpresa(CriarLocalizacaoEmpresaDTO localizacaoEmpresa)
        {
            if (localizacaoEmpresa.Location.Coordinates.Latitude == null || localizacaoEmpresa.Location.Coordinates.Longitude == null)
                throw new DomainException("Localização não cadastrada! preencha todos os campos.");

            _localizacaoRepo.AdicionarLocalizacaoEmpresa(LocalizacaoEmpresaParaDTO.ConverterLocalizacaoParaDto(localizacaoEmpresa));
        }

        public void AtualizarLocalizacaoEmpresa(int empresaId, AtualizarLocalizacaoEmpresaDTO localizacaoEmpresa)
        {
            if (localizacaoEmpresa.latitude == null || localizacaoEmpresa.longitude == null || localizacaoEmpresa.precisao == null)
                throw new DomainException("Localização não atualizada! preencha todos os campos.");

            _localizacaoRepo.AtualizarLocalizacaoEmpresa(empresaId, LocalizacaoEmpresaParaDTO.convertAtualizarLocalizacaoParaDto(localizacaoEmpresa));
        }
    }
}
