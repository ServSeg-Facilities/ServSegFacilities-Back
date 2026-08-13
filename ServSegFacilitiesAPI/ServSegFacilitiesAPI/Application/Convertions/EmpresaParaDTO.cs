using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs.EmpresaDTO;

namespace ServSegFacilitiesAPI.Application.Convertions
{
    public static class EmpresaParaDTO
    {
        public static empresa converterEmpresaParaDto(CriarEmpresaDTO empresa)
        {
            empresa empresaRet = new empresa
            {
                cnpj = empresa.cnpj,
                razaoSocial = empresa.razaoSocial,
                nomeFantasia = empresa.nomeFantasia,
                telefone = empresa.telefone,
                email = empresa.email,
                cep = empresa.cep,
                logradouro = empresa.logradouro,
                numero = empresa.numero,
                complemento = empresa.complemento,
                bairro = empresa.bairro,
                cidade = empresa.cidade,
                estado = empresa.estado
            };

            return empresaRet;
        }

        public static empresa converterEmpresaAtualizadaDto(AtualizarEmpresaDTO atualizarEmpresa)
        {
            empresa empresaRet = new empresa
            {
                cnpj = atualizarEmpresa.cnpj,
                razaoSocial = atualizarEmpresa.razaoSocial,
                nomeFantasia = atualizarEmpresa.nomeFantasia,
                telefone = atualizarEmpresa.telefone,
                email = atualizarEmpresa.email,
                cep = atualizarEmpresa.cep,
                logradouro = atualizarEmpresa.logradouro,
                numero = atualizarEmpresa.numero,
                complemento = atualizarEmpresa.complemento,
                bairro = atualizarEmpresa.bairro,
                cidade = atualizarEmpresa.cidade,
                estado = atualizarEmpresa.estado
            };
            return empresaRet;
        }
    }
}
