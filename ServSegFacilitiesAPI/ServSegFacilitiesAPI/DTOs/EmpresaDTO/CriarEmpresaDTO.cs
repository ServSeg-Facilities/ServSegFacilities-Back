using System.Text.Json.Serialization;

public class CriarEmpresaDTO
{
    [JsonPropertyName("cnpj_raiz")]
    public string Cnpj_Raiz { get; set; } = null!;

    [JsonPropertyName("razao_social")]
    public string Razao_Social { get; set; } = null!;

    [JsonPropertyName("nome_fantasia")]
    public string? Nome_Fantasia { get; set; }

    [JsonPropertyName("estabelecimento")]
    public EstabelecimentoDTO Estabelecimento { get; set; } = null!;
}

public class EstabelecimentoDTO
{
    [JsonPropertyName("cnpj")]
    public string Cnpj { get; set; } = null!;

    [JsonPropertyName("nome_fantasia")]
    public string? Nome_Fantasia { get; set; }

    [JsonPropertyName("telefone1")]
    public string? Telefone1 { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("cep")]
    public string Cep { get; set; } = null!;

    [JsonPropertyName("logradouro")]
    public string Logradouro { get; set; } = null!;

    [JsonPropertyName("numero")]
    public string Numero { get; set; } = null!;

    [JsonPropertyName("complemento")]
    public string? Complemento { get; set; }

    [JsonPropertyName("bairro")]
    public string Bairro { get; set; } = null!;

    [JsonPropertyName("cidade")]
    public CidadeDTO Cidade { get; set; } = null!;

    [JsonPropertyName("estado")]
    public EstadoDTO Estado { get; set; } = null!;
}

public class CidadeDTO
{
    [JsonPropertyName("nome")]
    public string Nome { get; set; } = null!;
}

public class EstadoDTO
{
    [JsonPropertyName("nome")]
    public string Nome { get; set; } = null!;
}