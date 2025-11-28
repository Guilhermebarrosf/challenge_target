using System.Text.Json.Serialization;
public class Root
{
    [JsonPropertyName("estoque")]
    public List<Product> Inventory { get; set; } = new List<Product>();
}

public class Product
{
    [JsonPropertyName("codigoProduto")]
    public int ProductCode { get; set; }

    [JsonPropertyName("descricaoProduto")]
    public string ProductDescription { get; set; } = string.Empty;

    [JsonPropertyName("estoque")]
    public int Stock { get; set; }
}