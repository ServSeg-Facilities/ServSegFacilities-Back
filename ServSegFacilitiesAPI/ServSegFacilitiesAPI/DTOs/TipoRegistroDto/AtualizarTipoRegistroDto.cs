using System.ComponentModel.DataAnnotations;

namespace ServSegFacilitiesAPI.DTOs.TipoRegistroDto
{
    public class AtualizarTipoRegistroDto
    {
        [Required(ErrorMessage = "O nome do tipo de registro é obrigatório!")]
        public string NomeTipoRegistro { get; set; } = string.Empty;
    }
}
