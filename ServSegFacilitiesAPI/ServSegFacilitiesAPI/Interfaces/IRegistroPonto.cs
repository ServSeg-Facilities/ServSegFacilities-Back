using ServSegFacilitiesAPI.Domains;

namespace ServSegFacilitiesAPI.Interfaces
{
    public interface IRegistroPonto
    {
        //buscar data
        //buscar empresa
        //listar pontos do usuario
        //adicionar
        //remover temporario

        List<registroPonto> Listar();
        List<registroPonto> ListarRegistrosPorUsuario(int id);
        //registroPonto BuscarPorID(int id);
        //registroPonto BuscarPorData(DateTime data);
        void Adicionar(registroPonto registroPonto);
        byte[] ObterImagem(int id);

        registroPonto? BuscarUltimoRegistro(int usuarioID);

    }
}
