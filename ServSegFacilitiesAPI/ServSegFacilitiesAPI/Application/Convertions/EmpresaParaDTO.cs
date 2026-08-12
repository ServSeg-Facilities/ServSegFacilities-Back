using ServSegFacilitiesAPI.Domains;
using ServSegFacilitiesAPI.DTOs.EmpresaDTO;

namespace ServSegFacilitiesAPI.Application.Convertions
{
    public static class EmpresaParaDTO
    {
        public static empresa converterEmpresaParaDto(CriarEmpresaDTO empresa, EstabelecimentoDTO estabelecimento, CidadeDTO cidade, EstadoDTO estado, Bairro)
        {
            empresa empresaRet = new empresa
            {
                cnpj = empresa.Cnpj_Raiz,
                razaoSocial = empresa.Razao_Social,
                nomeFantasia = empresa.Nome_Fantasia,
                telefone = estabelecimento.Telefone1,
                email = estabelecimento.Email,
                cep = estabelecimento.Cep,
                logradouro = estabelecimento.Logradouro,
                numero = estabelecimento.Numero,
                complemento = estabelecimento.Complemento,
                bairro = estabelecimento.Bairro,
                cidade = cidade.Nome,
                estado = estado.Nome
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
