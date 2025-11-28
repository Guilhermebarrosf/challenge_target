public class VendasPrinter
{
    public static void printerComission(List<Venda> vendas, int comission, double priceDetermined, bool receiverComission, bool belowCondition)
    {
        if (receiverComission)
        {
            Console.WriteLine($"\n\n--- Commission {comission}% {(belowCondition ? "<" : ">=")} R${priceDetermined} ---\n");
            foreach (var v in vendas)
            {
                Console.WriteLine($"Name: {v.Vendedor} - Value: R$ {v.Valor} - Comission: R$ {v.Valor * ((double)comission / 100):F2}");
            }
            return;
        }
        Console.WriteLine($"\n--- Without Commission {comission}% {(belowCondition ? "<" : ">=")} R${priceDetermined} ---\n");
        foreach (var v in vendas)
        {
            Console.WriteLine($"Name: {v.Vendedor} - Value: R$ {v.Valor}");
        }
    }

}
