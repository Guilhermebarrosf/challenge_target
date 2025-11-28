using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Spectre.Console;
using System.Text.Json.Serialization;

public class Program
{
    private static List<Product> currentInventory = new List<Product>();
    private static List<Movement> movementHistory = new List<Movement>();

    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        const string JSONPath = "product.json";

        if (!DataReader.loadInitialInventory(ref currentInventory)) return;

        var loopActive = true;
        while (loopActive)
        {
            AnsiConsole.Clear();

            var menu = new SelectionPrompt<string>()
                .PageSize(10)
                .AddChoices(new[] {
                    "1. Stock Inbound",
                    "2. Stock Outbound",
                    "3. View Current Stock",
                    "4. View Movement History",
                    "5. Exit"
                });

            string choice = AnsiConsole.Prompt(menu);

            switch (choice.Split('.')[0])
            {
                case "1":
                    InventoryProcessor.ProcessStockMovement(true, ref currentInventory, ref movementHistory);
                    break;
                case "2":
                    InventoryProcessor.ProcessStockMovement(false, ref currentInventory, ref movementHistory);
                    break;
                case "3":
                    InventoryProcessor.DisplayCurrentStock(currentInventory);
                    break;
                case "4":
                    InventoryProcessor.DisplayMovementHistory(movementHistory);
                    break;
                case "5":
                    loopActive = false;
                    break;
            }
        }
    }
}