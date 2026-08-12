using System.ComponentModel.DataAnnotations;

namespace ServSegFacilitiesAPI.DTOs.TipoRegistroDto
{
    public class ListarTipoRegistroDto
    {
        public int TipoRegistroId { get; set; }
        public string NomeTipoRegistro { get; set; } = string.Empty;
    }
}
