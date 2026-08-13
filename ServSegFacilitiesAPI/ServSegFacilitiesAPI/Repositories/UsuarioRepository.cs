using Microsoft.EntityFrameworkCore;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.Interfaces;
using ServSegFacilitiesAPI.Contexts;

namespace ServSegFacilitiesAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ServSeg_FacilitiesContext _context;

        public UsuarioRepository(ServSeg_FacilitiesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<usuario>> ListarTodos()
        {
            return await _context.usuario
                .Include(u => u.cargo)
                .Include(u => u.empresa)
                .ToListAsync();
        }

        public usuario BuscarPorId(int id)
        {
            Console.WriteLine($"ID recebido: {id}");

            var usuario = _context.usuario
                .Find(id);

            Console.WriteLine($"Resultado: {usuario?.nome ?? "NULL"}");

            return usuario;
        }

        public async Task Cadastrar(usuario novoUsuario)
        {
            await _context.usuario.AddAsync(novoUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(int id, usuario usuarioAtualizado)
        {
            var usuarioBuscado = await _context.usuario.FindAsync(id);

            if (usuarioBuscado != null)
            {
                usuarioBuscado.nome = usuarioAtualizado.nome;
                usuarioBuscado.email = usuarioAtualizado.email;
                usuarioBuscado.senha = usuarioAtualizado.senha;
                usuarioBuscado.cargoId = usuarioAtualizado.cargoId;
                usuarioBuscado.empresaId = usuarioAtualizado.empresaId;

                _context.usuario.Update(usuarioBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Deletar(int id)
        {
            var usuarioBuscado = await _context.usuario.FindAsync(id);
            if (usuarioBuscado != null)
            {
                _context.usuario.Remove(usuarioBuscado);
                await _context.SaveChangesAsync();
            }
        }
    }
}