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

        List <Venda>? vendas = VendasReader.readJsonVenda(pathJson);

        if (vendas == null) return;

        Console.WriteLine("=== Vendas reading concluded! ===\n");

        var vendasWithoutComission = VendasProcessor.calcVendaReceiverComission(vendas, minimumComissionCalc, true);

        var vendasWithComission = VendasProcessor.calcVendaReceiverComission(vendas, minimumComissionCalc, false);

        var vendasBelowPriceDetermined = VendasProcessor.calcVendasPriceDetermined(vendasWithComission, priceDeterminedComissionLow, true);

        var vendasUpPriceDetermined = VendasProcessor.calcVendasPriceDetermined(vendasWithComission, priceDeterminedComissionHigh, false);

        VendasPrinter.printerComission(vendasWithoutComission, 0, minimumComissionCalc, false, true);
        VendasPrinter.printerComission(vendasBelowPriceDetermined, lowComission, priceDeterminedComissionLow, true, true);
        VendasPrinter.printerComission(vendasUpPriceDetermined, highComission, priceDeterminedComissionHigh, true, false);

    }

}
