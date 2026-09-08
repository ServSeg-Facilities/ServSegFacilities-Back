using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs.LogHistoricoRegistroPontoDto;
using ServSegFacilitiesAPI.Exceptions;
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

        private ListarLogHistoricoRegistroPontoDto converterHistoricoParaDto(historicoRegistroPonto historico)
        {
            return new ListarLogHistoricoRegistroPontoDto
            {
                historicoId = historico.historicoId,
                registroPontoEntradaId = historico.registroPontoEntradaId,
                registroPontoSaidaId = historico.registroPontoSaidaId,
                nomeUsuario = historico.registroPontoEntrada.usuario.nome ?? string.Empty,
                nomeEmpresa = historico.registroPontoEntrada.usuario.empresa.razaoSocial ?? string.Empty,
                dataHoraPontoEntrada = historico.registroPontoEntrada.dataHoraPonto,
                dataHoraPontoSaida = historico.registroPontoSaida?.dataHoraPonto,
                latitudeEntrada = historico.registroPontoEntrada.latitude,
                latitudeSaida = historico.registroPontoSaida?.latitude,
                longitudeEntrada = historico.registroPontoEntrada.longitude,
                longitudeSaida = historico.registroPontoSaida?.longitude,
            };
        }

        public List<ListarLogHistoricoRegistroPontoDto> ObterHistoricoListagem(int usuarioId)
        {
            List<historicoRegistroPonto> historicos = _repository.ListarPorUsuario(usuarioId);

            if (historicos.Count <= 0)
                throw new DomainException("Nenhum registro encontrado!");

            return historicos.Select(hr => converterHistoricoParaDto(hr)).ToList();
        }

        public ListarLogHistoricoRegistroPontoDto ObterHistoricoPorId(int historicoId)
        {
            historicoRegistroPonto historico = _repository.ObterHistoricoPorId(historicoId)
                ?? throw new DomainException("Nenhum historíco localizado!");

            return converterHistoricoParaDto(historico);
        }
    }
}
