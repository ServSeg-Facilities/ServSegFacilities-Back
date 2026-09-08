using ServSegFacilitiesAPI.Domains;

namespace ServSegFacilitiesAPI.Interfaces
{
    public interface IHistoricoRegistroPonto
    {
        List<historicoRegistroPonto> ListarPorUsuario(int usuarioId);
        historicoRegistroPonto ObterHistoricoPorId(int historicoId);
    }
}
