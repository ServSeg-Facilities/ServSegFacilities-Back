namespace ServSegFacilitiesAPI.DTOs.LogHistoricoRegistroPontoDto
{
    public class ListarLogHistoricoRegistroPontoDto
    {
        // Adicionei aqui a listagem dos dois pontos de um
        // Adicionei aqui a listagem dos dois pontos de uma só vez
        public int historicoId { get; set; }

        public int registroPontoEntradaId { get; set; }

        public int? registroPontoSaidaId { get; set; }
        public DateTime dataHoraPontoEntrada { get; set; }
        public DateTime? dataHoraPontoSaida { get; set; }

        public double latitudeEntrada { get; set; }
        public double? latitudeSaida { get; set; }
        public double longitudeEntrada { get; set; }
        public double? longitudeSaida { get; set; }

        public string nomeUsuario { get; set; }
        public string nomeEmpresa { get; set; }
    }
}
