using System.Text.Json.Serialization;

public class Root
{
    [JsonPropertyName("vendas")]
    public List<Venda> Vendas { get; set; } = new List<Venda>();
}
public class Venda
{
    [JsonPropertyName("vendedor")]
    public string Vendedor {  get; set; } = string.Empty;

    [JsonPropertyName("valor")]
    public double Valor { get; set; } = double.NaN;
}