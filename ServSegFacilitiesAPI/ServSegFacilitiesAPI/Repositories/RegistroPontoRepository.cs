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

        public void Adicionar(registroPonto registro)
        {
            _context.registroPonto.Add(registro);
            _context.SaveChanges();
        }
        //public registroPonto BuscarPorID(int id)
        //{
        //    return _context.registroPonto.Find(id)!;
        //}
        public registroPonto? BuscarUltimoRegistro(int usuarioID)
        {
            return _context.registroPonto
                .Where(r => r.usuarioId == usuarioID)
                .OrderByDescending(r => r.dataHoraPonto)
                .FirstOrDefault();
        }
    }
}
