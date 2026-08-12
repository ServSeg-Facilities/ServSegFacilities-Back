using ServSegFacilitiesAPI.Domains;

namespace ServSegFacilitiesAPI.Interfaces
{
    public interface IEmpresaRepository
    {
        List<empresa> Listar();
        empresa ObterPorId(int id);
        void CriarEmpresa(empresa empresa);
        void AtualizarEmpresa(int id, empresa empresa);
    }
}
