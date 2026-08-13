using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs.LocalizacaoEmpresaDTO;

namespace ServSegFacilitiesAPI.Application.Convertions
{
    public static class LocalizacaoEmpresaParaDTO
    {
        public static localizacaoEmpresa ConverterLocalizacaoParaDto(CriarLocalizacaoEmpresaDTO criarLocalizacaoEmpresaDTO)
        {
            return new localizacaoEmpresa
            {
                ///latitude = criarLocalizacaoEmpresaDTO.latitude,
                //longitude = criarLocalizacaoEmpresaDTO.longitude,
                precisao = 100,
            };
        }

        public static localizacaoEmpresa convertAtualizarLocalizacaoParaDto(AtualizarLocalizacaoEmpresaDTO atualizarLocalizacaoEmpresaDTO)
        {
            return new localizacaoEmpresa
            {
                latitude = atualizarLocalizacaoEmpresaDTO.latitude,
                longitude = atualizarLocalizacaoEmpresaDTO.longitude,
                precisao = atualizarLocalizacaoEmpresaDTO.precisao
            };
        }
    }
}
