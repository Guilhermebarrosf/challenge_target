public class VendasProcessor
{
    public static List<Venda> calcVendaReceiverComission(List<Venda> vendas, double minimumComissionCalc, bool checkReceiver)
    {
        if (checkReceiver)
        {
            return vendas.Where(v => v.Valor < minimumComissionCalc).ToList();
        }
        return vendas.Where(v => v.Valor >= minimumComissionCalc).ToList();

    }

    public static List<Venda> calcVendasPriceDetermined(List<Venda> vendasWithComission, double priceDetermined, bool belowCondition)
    {
        if (belowCondition)
        {
            return vendasWithComission.Where(v => v.Valor < priceDetermined).ToList();

        }
        return vendasWithComission.Where(v => v.Valor >= priceDetermined).ToList();
    }
}