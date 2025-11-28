using System.Text.Json;

public class VendasReader
{
    public static List<Venda> readJsonVenda(string pathJson)
    {

        string json = File.ReadAllText(pathJson);

        Root? root = JsonSerializer.Deserialize<Root>(json);

        if (root == null || root.Vendas == null)
        {
            Console.WriteLine("Data JSON not found!");
            return null;
        }
        List<Venda> vendas = root.Vendas;
        return vendas;
    }
}