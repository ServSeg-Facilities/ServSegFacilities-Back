using Microsoft.EntityFrameworkCore;
using ServSegFacilitiesAPI.Contexts;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.Interfaces;

namespace ServSegFacilitiesAPI.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly ServSeg_FacilitiesContext _context;

        public EmpresaRepository(ServSeg_FacilitiesContext context)
        {
            _context = context;
        }

        public List<empresa> Listar()
        {
            return _context.empresa.ToList();
        }

        public empresa ObterPorId(int id)
        {
            return _context.empresa.Include(l => l.localizacaoEmpresa)
                                   .Include(l => l.usuario)
                                   .FirstOrDefault(e => e.empresaId == id);
        }

        public empresa ObterPorCNPJ(string cnpj)
        {
            return _context.empresa.Include(l => l.localizacaoEmpresa)
                                   .Include(l => l.usuario)
                                   .FirstOrDefault(e => e.cnpj == cnpj.Replace("/","").Replace(".", "").Replace("-", "").Replace(" ", ""));
        }

        public empresa ObterPorRazaoSocial(string razaoSocial)
        {
            return _context.empresa.Include(l => l.localizacaoEmpresa)
                                   .Include(l => l.usuario)
                                   .FirstOrDefault(e => e.razaoSocial.ToLower() == razaoSocial.ToLower());
        }

        public void CriarEmpresa(empresa empresa)
        {
            _context.empresa.Add(empresa);
            _context.SaveChanges();
        }

        public void AtualizarEmpresa(int id, empresa empresa)
        {
            var empresaExistente = _context.empresa.FirstOrDefault(e => e.empresaId == id);
            if (empresaExistente != null)
            {
                empresaExistente.nomeFantasia = empresa.nomeFantasia;
                empresaExistente.razaoSocial = empresa.razaoSocial;
                empresaExistente.cnpj = empresa.cnpj;
                empresaExistente.cep = empresa.cep;
                empresaExistente.bairro = empresa.bairro;
                empresaExistente.cidade = empresa.cidade;
                _context.SaveChanges();
            }
        }
    }
}
