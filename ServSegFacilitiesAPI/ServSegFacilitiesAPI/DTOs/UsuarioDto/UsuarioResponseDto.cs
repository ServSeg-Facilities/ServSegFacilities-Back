namespace ServSegFacilitiesAPI.DTOs
{
    public class UsuarioResponseDto
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int CargoId { get; set; }
        public string? NomeCargo { get; set; }
        public int EmpresaId { get; set; }
        public string? NomeEmpresa { get; set; }
    }
}