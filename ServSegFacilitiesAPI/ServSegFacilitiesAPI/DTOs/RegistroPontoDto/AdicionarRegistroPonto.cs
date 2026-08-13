using ServSegFacilitiesAPI.Domains;

namespace ServSegFacilitiesAPI.DTOs.RegistroPonto
{
    public class AdicionarRegistroPonto
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Precisao { get; set; }

        public int TipoRegistroId { get; set; }
    }
}
