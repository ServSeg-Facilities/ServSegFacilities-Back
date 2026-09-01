using ServSegFacilitiesAPI.Domains;

namespace ServSegFacilitiesAPI.Interfaces
{
    public interface IHistoricoRegistroPonto
    {
        Task<List<historicoRegistroPonto>> ListarPorUsuario(int usuarioId);
    }
}
