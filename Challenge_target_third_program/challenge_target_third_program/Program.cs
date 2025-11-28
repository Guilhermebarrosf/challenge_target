using System.Globalization;
using System;

public class CalculoJuros
{

    private static decimal obterValorDecimal(string prompt)
    {
        decimal valor;
        Console.Write(prompt);
        string input = Console.ReadLine();

        while (!decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out valor))
        {
            Console.WriteLine("Input Inválido");
            input = Console.ReadLine();
        }

        Console.WriteLine("You entered: " + valor);
        return valor;
    }

    private static DateTime obterData(string prompt)
    {
        DateTime data;
        Console.Write(prompt);
        string input = Console.ReadLine();

        while (!DateTime.TryParse(input, CultureInfo.CurrentCulture, DateTimeStyles.None, out data))
        {
            Console.WriteLine("Formato Inválido, por favor use o formato DD/MM/YYYY:");
            input = Console.ReadLine();
        }

        return data;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("--- CÁLCULO DE MULTA DIÁRIA ---");

        decimal valorOriginal = obterValorDecimal("Digite o valor original do débito: R$ ");
        DateTime dataVencimento = obterData("Digite a data de vencimento (DD/MM/AAAA): ");

        DateTime dataHoje = DateTime.Today;
        const decimal taxaMultaDiaria = 0.025m;

        TimeSpan diferenca = dataHoje - dataVencimento;
        int diasAtraso = diferenca.Days;

        if (diasAtraso <= 0)
        {
            Console.WriteLine("\nSituação: Pagamento em dia ou antecipado.");
            Console.WriteLine($"Valor a pagar hoje ({dataHoje:dd/MM/yyyy}): R$ {valorOriginal:N2}");
            return;
        }

            decimal valorJurosMulta = valorOriginal * taxaMultaDiaria * diasAtraso;
            decimal valorTotal = valorOriginal + valorJurosMulta;

            Console.WriteLine("\n--- RESULTADO DO CÁLCULO ---");
            Console.WriteLine($"Valor Original:    R$ {valorOriginal:N2}");
            Console.WriteLine($"Data Vencimento:   {dataVencimento:dd/MM/yyyy}");
            Console.WriteLine($"Data Atual:        {dataHoje:dd/MM/yyyy}");
            Console.WriteLine($"Dias de Atraso:    {diasAtraso}");
            Console.WriteLine($"Multa Total/Porcentagem Diária: R$ {valorJurosMulta:N2}/(2,5%)");
        
    }
}