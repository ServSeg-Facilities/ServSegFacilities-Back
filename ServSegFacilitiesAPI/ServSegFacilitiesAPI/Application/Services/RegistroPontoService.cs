using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs.RegistroPonto;
using ServSegFacilitiesAPI.Exceptions;
using ServSegFacilitiesAPI.Interfaces;
using System.Globalization;

namespace ServSegFacilitiesAPI.Application.Services
{
    public class RegistroPontoService
    {
        private readonly IRegistroPonto _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmpresaRepository _empresaRepository;
        private readonly ILocalizacaoEmpresaRepository _locRepository;

        public RegistroPontoService(
            IRegistroPonto repository,
            IUsuarioRepository usuarioRepository,
            IEmpresaRepository empresaRepository,
            ILocalizacaoEmpresaRepository locRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
            _empresaRepository = empresaRepository;
            _locRepository = locRepository;
        }

        public registroPonto Adicionar(int usuarioID, AdicionarRegistroPonto dto)
        {
            var usuario = _usuarioRepository.BuscarPorId(usuarioID)
                ?? throw new DomainException("Usuário não encontrado.");

            // 1. Validar Sequência de Entrada / Saída
            var ultimoRegistro = _repository.BuscarUltimoRegistro(usuarioID);

            if (ultimoRegistro == null)
            {
                if (dto.TipoRegistroId != 1)
                {
                    throw new DomainException("Para registrar uma saída, é preciso primeiro registrar uma entrada.");
                }
            }
            else
            {
                bool mesmoDia = ultimoRegistro.dataHoraPonto.Date == DateTime.Today;

                if (dto.TipoRegistroId == 2) // Tentando registrar Saída
                {
                    if (ultimoRegistro.tipoRegistroId == 2 || !mesmoDia)
                    {
                        throw new DomainException("Para registrar uma saída, é preciso primeiro registrar uma entrada.");
                    }
                }
                else if (dto.TipoRegistroId == 1) // Tentando registrar Entrada
                {
                    if (mesmoDia && ultimoRegistro.tipoRegistroId == 1)
                    {
                        throw new DomainException("Você já possui uma entrada registrada hoje. Para registrar novamente, registre a saída primeiro.");
                    }
                }
                else if (mesmoDia && ultimoRegistro.tipoRegistroId == dto.TipoRegistroId)
                {
                    throw new DomainException("Não é possível registrar o mesmo tipo de ponto duas vezes seguidas.");
                }
            }

            // 2. Validação de Raio de Distância OBRIGATÓRIA APENAS NA ENTRADA (TipoRegistroId == 1)
            // No ponto de Saída (ou outros), o limitador de range NÃO deve existir (pode bater de qualquer lugar).
            if (dto.TipoRegistroId == 1)
            {
                var empresa = _empresaRepository.ObterPorId(usuario.empresaId)
                    ?? throw new DomainException("Empresa do usuário não encontrada.");

                var localizacao = _locRepository.ObterPorEmpresaId(empresa.empresaId)
                    ?? throw new DomainException("Localização da empresa não cadastrada.");

                if (dto.Precisao > 200)
                {
                    throw new DomainException(
                        $"A precisão do seu GPS está imprecisa. " +
                        $"Precisão atual: {dto.Precisao:F2} metros. O limite é de 200 metros."
                    );
                }

                // Converter Coordenadas da Empresa
                if (!double.TryParse(localizacao.latitude, NumberStyles.Any, CultureInfo.InvariantCulture, out double latitudeEmpresa) ||
                    !double.TryParse(localizacao.longitude, NumberStyles.Any, CultureInfo.InvariantCulture, out double longitudeEmpresa))
                {
                    throw new DomainException("Coordenadas da empresa estão em um formato inválido.");
                }

                // Validar Raio de Distância até a Empresa (50 metros)
                double distancia = CalcularDistancia(
                    dto.Latitude,
                    dto.Longitude,
                    latitudeEmpresa,
                    longitudeEmpresa
                );

                if (distancia > 50)
                {
                    throw new DomainException(
                        $"Você está fora da área permitida para registrar a Entrada. " +
                        $"Distância até a empresa: {distancia:F2} metros. O limite é de 50 metros."
                    );
                }
            }

            // 3. Salvar Registro sempre com data e hora atual do servidor
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
            return registro;
        }

        private double CalcularDistancia(
            double latitude1,
            double longitude1,
            double latitude2,
            double longitude2)
        {
            const double raioTerra = 6371000; // Raio da Terra em metros

            double lat1 = latitude1 * Math.PI / 180;
            double lat2 = latitude2 * Math.PI / 180;

            double diferencaLatitude = (latitude2 - latitude1) * Math.PI / 180;
            double diferencaLongitude = (longitude2 - longitude1) * Math.PI / 180;

            double a = Math.Sin(diferencaLatitude / 2) * Math.Sin(diferencaLatitude / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Sin(diferencaLongitude / 2) * Math.Sin(diferencaLongitude / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return raioTerra * c;
        }

        public registroPonto? BuscarUltimoRegistro(int usuarioId)
        {
            return _repository.BuscarUltimoRegistro(usuarioId);
        }
    }
}
