using System.Text.Json.Serialization;

public class CriarEmpresaDTO
{
    [JsonPropertyName("cpnj")]
    public string cnpj { get; set; } = null!;

    [JsonPropertyName("razao_social")]
    public string razaoSocial { get; set; } = null!;

    [JsonPropertyName("nome_fantasia")]
    public string? nomeFantasia { get; set; }

    [JsonPropertyName("ddd_telefone_1")]
    public string? telefone { get; set; }

    [JsonPropertyName("email")]
    public string? email { get; set; }

    [JsonPropertyName("cep")]
    public string cep { get; set; } = null!;

    [JsonPropertyName("logradouro")]
    public string logradouro { get; set; } = null!;

    [JsonPropertyName("numero")]
    public string numero { get; set; } = null!;

    [JsonPropertyName("complemento")]
    public string? complemento { get; set; }

    [JsonPropertyName("bairro")]
    public string bairro { get; set; } = null!;

    [JsonPropertyName("municipio")]
    public string cidade { get; set; } = null!;

    [JsonPropertyName("uf")]
    public string estado { get; set; } = null!;
}