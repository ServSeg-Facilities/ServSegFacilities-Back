using Microsoft.EntityFrameworkCore;
using ServSegFacilitiesAPI.Contexts;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.Interfaces;

namespace ServSegFacilitiesAPI.Repositories
{
    public class HistoricoRegistroPontoRepository : IHistoricoRegistroPonto
    {
        private readonly ServSeg_FacilitiesContext _context;

        public HistoricoRegistroPontoRepository(ServSeg_FacilitiesContext context)
        {
            _context = context;
        }

        public async Task<List<historicoRegistroPonto>> ListarTodos()
        {
            return await _context.historicoRegistroPonto
            .Include(h => h.usuario)
                .ThenInclude(u => u.empresa)
            .Include(h => h.tipoRegistro)
            .AsNoTracking()
            .OrderByDescending(h => h.dataHoraPonto)
            .ToListAsync();
        }
    }
}
