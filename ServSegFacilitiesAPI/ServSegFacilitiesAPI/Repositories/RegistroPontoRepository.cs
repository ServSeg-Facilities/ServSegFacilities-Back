using Microsoft.EntityFrameworkCore;
using ServSegFacilitiesAPI.Contexts;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.Interfaces;

namespace ServSegFacilitiesAPI.Repositories
{
    public class RegistroPontoRepository : IRegistroPonto
    {
        private readonly ServSeg_FacilitiesContext _context;
        public RegistroPontoRepository(ServSeg_FacilitiesContext context)
        {
            _context = context;
        }

        public List<registroPonto> Listar()
        {
            return _context.registroPonto.Include(r => r.usuario)
                                         .Include(r => r.usuario.empresa)
                                         .Include(r => r.tipoRegistro)
                                         .ToList();
        }

        public List<registroPonto> ListarRegistrosPorUsuario(int usuarioId)
        {
            return _context.registroPonto.Where(r => r.usuarioId.Equals(usuarioId))
                .Include(r => r.usuario)
                .Include(r => r.usuario.empresa)
                .Include(r => r.tipoRegistro)
                .ToList();
        }

        public void Adicionar(registroPonto registro)
        {
            _context.registroPonto.Add(registro);
            _context.SaveChanges();
        }

        public registroPonto? BuscarUltimoRegistro(int usuarioID)
        {
            return _context.registroPonto
                .Where(r => r.usuarioId == usuarioID)
                .OrderByDescending(r => r.dataHoraPonto)
                .FirstOrDefault();
        }
        public byte[] ObterImagem(int id)
        {
            var registroPonto = _context.registroPonto
                .Where(r => r.registroPontoId == id)
                .Select(p => p.fotoPonto)
                .FirstOrDefault();

            return registroPonto;
        }
    }
}
