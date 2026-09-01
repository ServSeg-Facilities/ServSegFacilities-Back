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

        public localizacaoEmpresa? ObterPorEmpresaId(int empresaId)
        {
            return _context.localizacaoEmpresa.FirstOrDefault(l => l.empresaId == empresaId);
        }

        public void AdicionarLocalizacaoEmpresa(localizacaoEmpresa localizacaoEmpresa)
        {
            _context.localizacaoEmpresa.Add(localizacaoEmpresa);
            _context.SaveChanges();
        }

        public void AtualizarLocalizacaoEmpresa(int empresaId, localizacaoEmpresa localizacaoEmpresa)
        {
            var localizacaoExistente = _context.localizacaoEmpresa.FirstOrDefault(l => l.empresaId == empresaId);
            if (localizacaoExistente != null)
            {
                localizacaoExistente.latitude = localizacaoEmpresa.latitude;
                localizacaoExistente.longitude = localizacaoEmpresa.longitude;
                localizacaoExistente.precisao = localizacaoEmpresa.precisao;
                _context.SaveChanges();
            }
        }
    }
}
