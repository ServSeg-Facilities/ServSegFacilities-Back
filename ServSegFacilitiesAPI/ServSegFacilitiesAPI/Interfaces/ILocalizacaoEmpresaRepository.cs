using ServSegFacilitiesAPI.Domains;

namespace ServSegFacilitiesAPI.Interfaces
{
    public interface ILocalizacaoEmpresaRepository
    {
        localizacaoEmpresa ObterPorEmpresaId(int empresaId);
        void AdicionarLocalizacaoEmpresa(int empresaId, localizacaoEmpresa localizacaoEmpresa);
        void AtualizarLocalizacaoEmpresa(int empresaId, localizacaoEmpresa localizacaoEmpresa);
    }
}
