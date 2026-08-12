using ServSegFacilitiesAPI.Contexts;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.Interfaces;

namespace ServSegFacilitiesAPI.Repositories
{
    public class TipoRegistroRepository : ITipoRegistro
    {
        private readonly ServSeg_FacilitiesContext _context;
        public TipoRegistroRepository(ServSeg_FacilitiesContext context)
        {
            _context = context;
        }
        public List<tipoRegistro> Listar()
        {
            return _context.tipoRegistro.ToList();
        }
        public tipoRegistro BuscarPorID(int tipoRegistroID)
        {
            return _context.tipoRegistro.Find(tipoRegistroID)!;
        }
        public void Adicionar(tipoRegistro tipoRegistro)
        {
            _context.tipoRegistro.Add(tipoRegistro);
            _context.SaveChanges();
        }
        public void Atualizar(tipoRegistro tipoRegistro)
        {
            if (tipoRegistro == null)
            {
                return;
            }
            tipoRegistro tipoRegistroBanco = _context.tipoRegistro.Find(tipoRegistro.tipoRegistroId)!;
            tipoRegistroBanco.nomeTipoRegistro = tipoRegistro.nomeTipoRegistro;
            _context.SaveChanges();
        }
        public bool TipoRegistroExiste(int id)
        {
            return _context.tipoRegistro.Any(tipoRegistro => tipoRegistro.tipoRegistroId == id);
        }
    }
}
