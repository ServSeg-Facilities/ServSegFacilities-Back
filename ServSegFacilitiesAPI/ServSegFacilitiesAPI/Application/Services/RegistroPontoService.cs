using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs.RegistroPonto;
using ServSegFacilitiesAPI.Exceptions;
using ServSegFacilitiesAPI.Interfaces;

namespace ServSegFacilitiesAPI.Application.Services
{
    public class RegistroPontoService
    {
        private readonly IRegistroPonto _repository;
        public RegistroPontoService(IRegistroPonto repository)
        {
            _repository = repository;
        }
        public void Adicionar(
        int usuarioID,
        AdicionarRegistroPonto dto)
        {
            // 1. Buscar último registro
            var ultimoRegistro =
                _repository.BuscarUltimoRegistro(usuarioID);

            //Validar Entrada / Saída
            if (ultimoRegistro == null)
            {
                if (dto.TipoRegistroId != 1)
                {
                    throw new DomainException("O primeiro registro deve ser uma entrada.");
                }
            }
            else
            {
                if (ultimoRegistro.tipoRegistroId == dto.TipoRegistroId)
                {
                    throw new DomainException("Não é possível registrar o mesmo tipo de ponto duas vezes seguidas.");
                }
            }

            if (dto.Precisao > 50)
            {
                throw new DomainException(
                    $"A localização está imprecisa. " +
                    $"Precisão atual: {dto.Precisao:F2} metros."
                );
            }

            //Localização temporária da empresa
            double latitudeEmpresa = -23.550520;
            double longitudeEmpresa = -46.633308;

            double distancia = CalcularDistancia(
                dto.Latitude,
                dto.Longitude,
                latitudeEmpresa,
                longitudeEmpresa
            );

            if (distancia > 50)
            {
                throw new DomainException(
                    $"Você está fora da área permitida. " +
                    $"Distância até a empresa: {distancia:F2} metros."
                );
            }

            var registro = new registroPonto
            {
                usuarioId = usuarioID,
                latitude = dto.Latitude,
                longitude = dto.Longitude,
                precisao = dto.Precisao,
                dataHoraPonto = DateTime.Now,
                status = true,
                tipoRegistroId = dto.TipoRegistroId
            };

            _repository.Adicionar(registro);
        }


        private double CalcularDistancia(
            double latitude1,
            double longitude1,
            double latitude2,
            double longitude2)
        {
            const double raioTerra = 6371000;

            double lat1 = latitude1 * Math.PI / 180;
            double lat2 = latitude2 * Math.PI / 180;

            double diferencaLatitude =
                (latitude2 - latitude1) * Math.PI / 180;

            double diferencaLongitude =
                (longitude2 - longitude1) * Math.PI / 180;

            double a =
                Math.Sin(diferencaLatitude / 2) *
                Math.Sin(diferencaLatitude / 2)
                +
                Math.Cos(lat1) *
                Math.Cos(lat2) *
                Math.Sin(diferencaLongitude / 2) *
                Math.Sin(diferencaLongitude / 2);

            double c =
                2 * Math.Atan2(
                    Math.Sqrt(a),
                    Math.Sqrt(1 - a)
                );

            return raioTerra * c;
        }
    }
}
