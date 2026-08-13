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

        //List<registroPonto> Listar();
        //registroPonto BuscarPorID(int id);
        //registroPonto BuscarPorData(DateTime data);
        void Adicionar(registroPonto registroPonto);
        registroPonto? BuscarUltimoRegistro(int usuarioID);

    }
}
