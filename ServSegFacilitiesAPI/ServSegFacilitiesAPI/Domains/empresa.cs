using System;
using System.Collections.Generic;

namespace ServSegFacilitiesAPI.Domains;

public partial class empresa
{
    public int empresaId { get; set; }

    public string cnpj { get; set; } = null!;

    public string razaoSocial { get; set; } = null!;

    public string? nomeFantasia { get; set; }

    public string? telefone { get; set; }

    public string? email { get; set; }

    public string cep { get; set; } = null!;

    public string logradouro { get; set; } = null!;

    public string numero { get; set; } = null!;

    public string? complemento { get; set; }

    public string bairro { get; set; } = null!;

    public string cidade { get; set; } = null!;

    public string estado { get; set; } = null!;

    public virtual ICollection<localizacaoEmpresa> localizacaoEmpresa { get; set; } = new List<localizacaoEmpresa>();

    public virtual ICollection<usuario> usuario { get; set; } = new List<usuario>();
}
