namespace ServSegFacilitiesAPI.DTOs.RegistroPontoDto
{
    public class ListarRegistrosDto
    {
        public int registroPontoId { get; set; }

        public string nomeUsuario { get; set; }
        public string nomeEmpresa { get; set; }
        public string tipoRegistro { get; set; }
        public double latitude { get; set; }
        public double logitude { get; set; }
        public DateTime dataHoraPonto { get; set; }

        public bool statusRegistroPonto { get; set; }

        public int tipoRegistroId { get; set; }
    }
}
