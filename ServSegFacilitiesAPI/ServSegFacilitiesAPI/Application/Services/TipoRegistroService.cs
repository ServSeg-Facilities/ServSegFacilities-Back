using ServSegFacilitiesAPI.Application.Regras;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs.TipoRegistroDto;
using ServSegFacilitiesAPI.Exceptions;
using ServSegFacilitiesAPI.Interfaces;

namespace ServSegFacilitiesAPI.Application.Services
{
    public class TipoRegistroService
    {
        private readonly ITipoRegistro _repository;
        public TipoRegistroService(ITipoRegistro repository)
        {
            _repository = repository;
        }
        public List<ListarTipoRegistroDto> Listar()
        {
            List<tipoRegistro> listaBanco = _repository.Listar();

            List<ListarTipoRegistroDto> listaDto = listaBanco.Select(item => new ListarTipoRegistroDto
            {
                TipoRegistroId = item.tipoRegistroId,
                NomeTipoRegistro = item.nomeTipoRegistro

            }).ToList();

            return listaDto;
        }
        public ListarTipoRegistroDto BuscarPorId(int id)
        {
            tipoRegistro buscaBanco = _repository.BuscarPorID(id);

            ListarTipoRegistroDto listar = new ListarTipoRegistroDto
            {
                TipoRegistroId = buscaBanco.tipoRegistroId,
                NomeTipoRegistro = buscaBanco.nomeTipoRegistro
            };
            return listar;
        }
        public void Adicionar(AdicionarTipoRegistroDto tipoRegistroDto)
        {
            Validar.ValidarNome(tipoRegistroDto.NomeTipoRegistro);

            tipoRegistro tipo = new tipoRegistro
            {
                nomeTipoRegistro = tipoRegistroDto.NomeTipoRegistro
            };
            _repository.Adicionar(tipo);
        }
        public void Atualizar(int id, AtualizarTipoRegistroDto dto)
        {
            tipoRegistro buscaBanco = _repository.BuscarPorID(id);
            if(buscaBanco == null)
            {
                throw new DomainException("Tipo de registro não encontrado.");
            }
            buscaBanco.nomeTipoRegistro = dto.NomeTipoRegistro;
            _repository.Atualizar(buscaBanco);
        }
    }
}
