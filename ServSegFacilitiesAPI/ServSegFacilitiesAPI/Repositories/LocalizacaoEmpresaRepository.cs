using ServSegFacilitiesAPI.Contexts;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.Interfaces;

namespace ServSegFacilitiesAPI.Repositories
{
    public class LocalizacaoEmpresaRepository : ILocalizacaoEmpresaRepository
    {
        private readonly ServSeg_FacilitiesContext _context;
        public LocalizacaoEmpresaRepository(ServSeg_FacilitiesContext context)
        {
            _context = context;
        }

        public localizacaoEmpresa ObterPorEmpresaId(int id)
        {
            return _context.localizacaoEmpresa.Find(id);
        }

        public void AdicionarLocalizacaoEmpresa(int empresaId, localizacaoEmpresa localizacaoEmpresa)
        {
            _context.Add(localizacaoEmpresa);
            _context.SaveChanges();
        }

        public void AtualizarLocalizacaoEmpresa(int empresaId, localizacaoEmpresa localizacaoEmpresa)
        {
            _context.Update(localizacaoEmpresa);
            _context.SaveChanges();
        }
    }
}
