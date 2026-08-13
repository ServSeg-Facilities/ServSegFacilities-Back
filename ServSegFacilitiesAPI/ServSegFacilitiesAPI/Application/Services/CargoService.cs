using System.Collections.Generic;
using System.Threading.Tasks;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs;
using ServSegFacilitiesAPI.Exceptions;
using ServSegFacilitiesAPI.Interfaces;

namespace ServSegFacilitiesAPI.Application.Services
{
    public class CargoService
    {
        private readonly ICargoRepository _repository;

        public CargoService(ICargoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<cargo>> ListarTodos()
        {
            return await _repository.ListarTodos();
        }

        public async Task<cargo?> BuscarPorId(int id)
        {
            return await _repository.BuscarPorId(id);
        }

        public async Task Cadastrar(CargoCriarDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NomeCargo))
            {
                throw new DomainException("O nome do cargo é obrigatório.");
            }

            var novoCargo = new cargo
            {
                nomeCargo = dto.NomeCargo.Trim()
            };

            await _repository.Cadastrar(novoCargo);
        }

        public async Task Atualizar(int id, CargoCriarDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.NomeCargo))
            {
                throw new DomainException("O nome do cargo é obrigatório.");
            }

            var cargoExistente = await _repository.BuscarPorId(id);
            if (cargoExistente == null)
            {
                throw new DomainException("Cargo não encontrado.");
            }

            cargoExistente.nomeCargo = dto.NomeCargo.Trim();
            await _repository.Atualizar(cargoExistente);
        }

        public async Task Deletar(int id)
        {
            var cargoExistente = await _repository.BuscarPorId(id);
            if (cargoExistente == null)
            {
                throw new DomainException("Cargo não encontrado.");
            }

            await _repository.Deletar(cargoExistente);
        }
    }
}