using System.Collections.Generic;
using System.Threading.Tasks;
using ServSegFacilitiesAPI.Domains;

namespace ServSegFacilitiesAPI.Interfaces
{
    public interface ICargoRepository
    {
        Task<List<cargo>> ListarTodos();
        Task<cargo?> BuscarPorId(int id);
        Task Cadastrar(cargo cargo);
        Task Atualizar(cargo cargo);
        Task Deletar(cargo cargo);
    }
}