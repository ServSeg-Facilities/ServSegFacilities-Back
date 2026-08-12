namespace ServSegFacilitiesAPI.DTOs.LocalizacaoEmpresaDTO
{
    public class CriarLocalizacaoEmpresaDTO
    {
        public int empresaId { get; set; }

        public string latitude { get; set; } = null!;

        public string longitude { get; set; } = null!;
    }
}
