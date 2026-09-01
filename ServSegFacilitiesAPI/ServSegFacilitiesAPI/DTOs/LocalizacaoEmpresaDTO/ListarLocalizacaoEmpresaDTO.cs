namespace ServSegFacilitiesAPI.DTOs.LocalizacaoEmpresaDTO
{
    public class ListarLocalizacaoEmpresaDTO
    {
        public int localizacaoEmpresaId { get; set; }

        public int empresaId { get; set; }

        public string latitude { get; set; } = null!;

        public string longitude { get; set; } = null!;

        public decimal? precisao { get; set; }
    }
}
