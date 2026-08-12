using System.ComponentModel.DataAnnotations;

namespace ServSegFacilitiesAPI.DTOs
{
    public class UsuarioCriarDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Nome { get; set; } = null!;

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Senha { get; set; } = null!;

        [Required(ErrorMessage = "O cargo é obrigatório.")]
        public int CargoId { get; set; }

        [Required(ErrorMessage = "A empresa é obrigatória.")]
        public int EmpresaId { get; set; }
    }
}