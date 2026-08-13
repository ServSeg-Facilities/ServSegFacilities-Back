using ServSegFacilitiesAPI.Domains;

namespace ServSegFacilitiesAPI.Interfaces
{
    public interface ILocalizacaoEmpresaRepository
    {
        localizacaoEmpresa ObterPorLocalizacaoEmpresaId(int empresaId);
        void AdicionarLocalizacaoEmpresa(localizacaoEmpresa localizacaoEmpresa);
        void AtualizarLocalizacaoEmpresa(int empresaId, localizacaoEmpresa localizacaoEmpresa);
    }
}
