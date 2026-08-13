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

        public localizacaoEmpresa ObterPorLocalizacaoEmpresaId(int id)
        {
            return _context.localizacaoEmpresa.Find(id);
        }

        public void AdicionarLocalizacaoEmpresa(int empresaId, localizacaoEmpresa localizacaoEmpresa)
        {
            _context.localizacaoEmpresa.Add(localizacaoEmpresa);
            _context.SaveChanges();
        }

        public void AtualizarLocalizacaoEmpresa(int empresaId, localizacaoEmpresa localizacaoEmpresa)
        {
            _context.localizacaoEmpresa.Update(localizacaoEmpresa);
            _context.SaveChanges();
        }
    }
}
