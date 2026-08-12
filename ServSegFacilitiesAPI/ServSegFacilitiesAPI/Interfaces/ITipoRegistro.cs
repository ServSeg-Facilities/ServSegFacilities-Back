using ServSegFacilitiesAPI.Domains;

namespace ServSegFacilitiesAPI.Interfaces
{
    public interface ITipoRegistro
    {
        List<tipoRegistro> Listar();
        tipoRegistro BuscarPorID(int id);
        void Adicionar(tipoRegistro tipoRegistro);
        void Atualizar(tipoRegistro tipoRegistro);
        bool TipoRegistroExiste(int tipoRegistroID);
    }
}
