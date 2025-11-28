using System.Text.Json;
using System.Linq;
using System.Xml;

class Program
{
    static void Main()
    {
        string pathJson = "Vendas.json";
        double priceDeterminedComissionLow = 500.00;
        double priceDeterminedComissionHigh = 500.00;
        double minimumComissionCalc = 100.00;
        int lowComission = 1;
        int highComission = 5;

        if (!File.Exists(pathJson))
        {
            Console.WriteLine("File not found!");
            return;
        }

        string json = File.ReadAllText(pathJson);

        Root? root = JsonSerializer.Deserialize<Root>(json);

        if (root == null || root.Vendas == null)
        {
            Console.WriteLine("Data JSON not found!");
            return;
        }
        List<Venda> vendas = root.Vendas;

        Console.WriteLine("=== Vendas reading concluded! ===\n");

        var vendasWithoutComission =
            vendas.Where(v => v.Valor <= minimumComissionCalc).ToList();

        var vendasWithComission =
            vendas.Where(v => v.Valor > minimumComissionCalc).ToList();

        var vendasBelowPriceDetermined =
            vendasWithComission.Where(v => v.Valor < priceDeterminedComissionLow).ToList();

        var vendasUpPriceDetermined =
            vendasWithComission.Where(v => v.Valor >= priceDeterminedComissionHigh).ToList();

        Console.WriteLine($"\n--- Without Commission (<= R${minimumComissionCalc}) ---\n");
        foreach (var v in vendasWithoutComission)
            Console.WriteLine($"Name: {v.Vendedor} - Value: R$ {v.Valor}");

        Console.WriteLine($"\n\n--- Commission {lowComission}% (< R${priceDeterminedComissionLow}) ---\n");
        foreach (var v in vendasBelowPriceDetermined)
            Console.WriteLine($"Name: {v.Vendedor} - Value: R$ {v.Valor} - Comission: R$ {v.Valor * ((double)lowComission /100):F2}");

        Console.WriteLine($"\n\n--- Commission {highComission}% (>= R${priceDeterminedComissionHigh}) --- ");
        foreach (var v in vendasUpPriceDetermined)
            Console.WriteLine($"Name: {v.Vendedor} - Value: R$ {v.Valor} - Comission: R$ {v.Valor * ((double)highComission /100):F2}");
    }
}
