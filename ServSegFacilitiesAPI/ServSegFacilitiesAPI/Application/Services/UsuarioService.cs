using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ServSegFacilitiesAPI.Contexts;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs;
using ServSegFacilitiesAPI.Exceptions;

namespace ServSegFacilitiesAPI.Application.Services
{
    public class UsuarioService
    {
        private readonly ServSeg_FacilitiesContext _context;

        public UsuarioService(ServSeg_FacilitiesContext context)
        {
            _context = context;
        }

        private static string HashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new DomainException("Senha é obrigatória.");
            }

            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToHexString(bytes);
        }

        public async Task<IEnumerable<UsuarioResponseDto>> ListarTodos()
        {
            return await _context.usuario
                .AsNoTracking()
                .Select(u => new UsuarioResponseDto
                {
                    UsuarioId = u.usuarioId,
                    Nome = u.nome,
                    Email = u.email,
                    CargoId = u.cargoId,
                    NomeCargo = u.cargo.nomeCargo,
                    EmpresaId = u.empresaId,
                    NomeEmpresa = u.empresa.nomeFantasia ?? u.empresa.razaoSocial
                })
                .ToListAsync();
        }

        public async Task<UsuarioResponseDto?> BuscarPorId(int id)
        {
            return await _context.usuario
                .AsNoTracking()
                .Where(u => u.usuarioId == id)
                .Select(u => new UsuarioResponseDto
                {
                    UsuarioId = u.usuarioId,
                    Nome = u.nome,
                    Email = u.email,
                    CargoId = u.cargoId,
                    NomeCargo = u.cargo.nomeCargo,
                    EmpresaId = u.empresaId,
                    NomeEmpresa = u.empresa.nomeFantasia ?? u.empresa.razaoSocial
                })
                .FirstOrDefaultAsync();
        }

        public async Task Cadastrar(UsuarioCriarDto dto)
        {
            var emailExiste = await _context.usuario.AnyAsync(u => u.email == dto.Email);
            if (emailExiste)
            {
                throw new DomainException("Já existe um usuário cadastrado com este e-mail.");
            }

            var novoUsuario = new usuario
            {
                nome = dto.Nome,
                email = dto.Email,
                senha = HashSenha(dto.Senha),
                cargoId = dto.CargoId,
                empresaId = dto.EmpresaId
            };

            await _context.usuario.AddAsync(novoUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(int id, UsuarioCriarDto dto)
        {
            var usuarioExistente = await _context.usuario.FindAsync(id);

            if (usuarioExistente == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            var emailEmUso = await _context.usuario.AnyAsync(u => u.email == dto.Email && u.usuarioId != id);
            if (emailEmUso)
            {
                throw new DomainException("O e-mail informado já está em uso por outro usuário.");
            }

            usuarioExistente.nome = dto.Nome;
            usuarioExistente.email = dto.Email;
            usuarioExistente.senha = HashSenha(dto.Senha);
            usuarioExistente.cargoId = dto.CargoId;
            usuarioExistente.empresaId = dto.EmpresaId;

            _context.usuario.Update(usuarioExistente);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(int id)
        {
            var usuarioExistente = await _context.usuario.FindAsync(id);

            if (usuarioExistente == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            _context.usuario.Remove(usuarioExistente);
            await _context.SaveChangesAsync();
        }
    }
}