using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServSegFacilitiesAPI.Contexts;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.Interfaces;

namespace ServSegFacilitiesAPI.Repositories
{
    public class CargoRepository : ICargoRepository
    {
        private readonly ServSeg_FacilitiesContext _context;

        public CargoRepository(ServSeg_FacilitiesContext context)
        {
            _context = context;
        }

        public async Task<List<cargo>> ListarTodos()
        {
            return await _context.cargo.ToListAsync();
        }

        public async Task<cargo?> BuscarPorId(int id)
        {
            return await _context.cargo.FindAsync(id);
        }

        public async Task Cadastrar(cargo cargo)
        {
            _context.cargo.Add(cargo);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(cargo cargo)
        {
            _context.cargo.Update(cargo);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(cargo cargo)
        {
            _context.cargo.Remove(cargo);
            await _context.SaveChangesAsync();
        }
    }
}