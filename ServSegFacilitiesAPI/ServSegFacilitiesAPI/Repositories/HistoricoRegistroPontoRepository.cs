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

        public List<historicoRegistroPonto> ListarPorUsuario(int usuarioId)
        {
            return _context.historicoRegistroPonto
                                            .Include(h => h.registroPontoEntrada)
                                                .ThenInclude(r => r.usuario)
                                                    .ThenInclude(u => u.empresa)
                                            .Include(h => h.registroPontoSaida)
                                            .Where(h => h.registroPontoEntrada.usuarioId == usuarioId)
                                            .OrderByDescending(h => h.registroPontoEntrada.dataHoraPonto)
                                            .ToList();
        }

        public historicoRegistroPonto ObterHistoricoPorId(int historicoId)
        {
            return _context.historicoRegistroPonto
                                           .Include(h => h.registroPontoEntrada)
                                               .ThenInclude(r => r.usuario)
                                                   .ThenInclude(u => u.empresa)
                                           .Include(h => h.registroPontoSaida)
                                           .FirstOrDefault(h => h.historicoId == historicoId);
        }
    }
}
