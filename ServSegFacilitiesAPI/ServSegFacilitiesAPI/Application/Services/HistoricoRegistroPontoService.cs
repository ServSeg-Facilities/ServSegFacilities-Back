using ServSegFacilitiesAPI.DTOs.LogHistoricoRegistroPontoDto;
using ServSegFacilitiesAPI.Interfaces;

namespace ServSegFacilitiesAPI.Application.Services
{
    public class HistoricoRegistroPontoService
    {
        private readonly IHistoricoRegistroPonto _repository;

        public HistoricoRegistroPontoService(IHistoricoRegistroPonto repository)
        {
            _repository = repository;
        }

        public async Task<List<ListarLogHistoricoRegistroPontoDto>> ObterHistoricoListagemAsync()
        {
            var historicos = await _repository.ListarTodos();

            var dtos = historicos.Select(h => new ListarLogHistoricoRegistroPontoDto
            {
                HistoricoId = h.historicoId,
                RegistroPontoId = h.registroPontoId,
                NomeUsuario = h.usuario?.nome ?? string.Empty,
                NomeEmpresa = h.usuario?.empresa?.razaoSocial ?? string.Empty,
                TipoRegistro = h.tipoRegistro?.nomeTipoRegistro ?? string.Empty,
                DataHoraPonto = h.dataHoraPonto,
                Latitude = h.latitude,
                Longitude = h.longitude,
                Precisao = h.precisao,
                Status = h.status
            }).ToList();

            return dtos;
        }
    }
}
