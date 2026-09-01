namespace ServSegFacilitiesAPI.DTOs.LocalizacaoEmpresaDTO
{
    public class AtualizarLocalizacaoEmpresaDTO
    {
        public string latitude { get; set; } = null!;

        public string longitude { get; set; } = null!;

        public decimal? precisao { get; set; }
    }
}
