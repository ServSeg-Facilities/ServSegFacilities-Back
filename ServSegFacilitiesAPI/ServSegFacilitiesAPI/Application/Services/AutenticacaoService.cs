using Microsoft.EntityFrameworkCore;
using ServSegFacilitiesAPI.Application.Autenticacao;
using ServSegFacilitiesAPI.Contexts;
using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs;
using ServSegFacilitiesAPI.DTOs.AutenticacaoDto;
using ServSegFacilitiesAPI.Exceptions;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ServSegFacilitiesAPI.Application.Services
{
    public class AutenticacaoService
    {
        private readonly ServSeg_FacilitiesContext _context;
        private readonly GeradorTokenJwt _tokenJwt;

        public AutenticacaoService(ServSeg_FacilitiesContext context, GeradorTokenJwt tokenJwt)
        {
            _context = context;
            _tokenJwt = tokenJwt;
        }

        private static string HashSenha(string senha)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
            return Convert.ToHexString(bytes);
        }

        private static bool VerificarSenha(string senhaDigitada, string senhaHashBanco)
        {
            return string.Equals(HashSenha(senhaDigitada), senhaHashBanco, StringComparison.OrdinalIgnoreCase);
        }

        public async Task<TokenDto> Login(LoginDto loginDto)
        {
            usuario? usuario = await _context.usuario
                .Include(u => u.cargo)
                .FirstOrDefaultAsync(u => u.email == loginDto.Email);

            if (usuario == null)
            {
                throw new DomainException("E-mail ou senha inválidos");
            }

            if (!VerificarSenha(loginDto.Senha, usuario.senha))
            {
                throw new DomainException("E-mail ou senha inválidos");
            }

            var token = _tokenJwt.GerarToken(usuario);

            return new TokenDto { Token = token };
        }
    }
}