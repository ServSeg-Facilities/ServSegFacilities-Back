using ServSegFacilitiesAPI.Exceptions;

namespace ServSegFacilitiesAPI.Application.Regras
{
    public class Validar
    {
        public static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome é obrigatório!");
            }
        }
    }
}
