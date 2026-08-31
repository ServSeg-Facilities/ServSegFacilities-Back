namespace ServSegFacilitiesAPI.DTOs.LogHistoricoRegistroPontoDto
{
    public class ListarLogHistoricoRegistroPontoDto
    {
            public int HistoricoId { get; set; }
            public int RegistroPontoId { get; set; }
            public string NomeUsuario { get; set; } = string.Empty;
            public string NomeEmpresa { get; set; } = string.Empty;
            public string TipoRegistro { get; set; } = string.Empty;
            public DateTime DataHoraPonto { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public double Precisao { get; set; }
            public bool Status { get; set; }
        }
    }
