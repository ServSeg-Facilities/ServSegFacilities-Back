using System.Text.Json.Serialization;

namespace ServSegFacilitiesAPI.DTOs.LocalizacaoEmpresaDTO
{
    using System.Text.Json.Serialization;

    public class CriarLocalizacaoEmpresaDTO
    {
        [JsonPropertyName("location")]
        public LocationDTO Location { get; set; } = null!;
    }

    public class LocationDTO
    {
        [JsonPropertyName("coordinates")]
        public CoordinatesDTO Coordinates { get; set; } = null!;
    }

    public class CoordinatesDTO
    {
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; } = null!;

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; } = null!;
    }
}
