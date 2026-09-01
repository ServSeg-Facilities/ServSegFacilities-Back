using ServSegFacilitiesAPI.Domains;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServSegFacilitiesAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<usuario>> ListarTodos();
        usuario BuscarPorId(int id);
        Task Cadastrar(usuario novoUsuario);
        Task Atualizar(int id, usuario usuarioAtualizado);
        Task Deletar(int id);
    }
}