using Spectre.Console;
using Spectre.Console.Rendering;
using System.Linq;
using System.Collections.Generic;

public static class InventoryProcessor
{
    public static void ProcessStockMovement(bool isStockIn, ref List<Product> currentInventory, ref List<Movement> movementHistory)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine(isStockIn ? "[yellow]>> ESTOQUE INBOUND ENTRY <<[/]" : "[yellow]>> ESTOQUE OUTBOUND ENTRY <<[/]");
        var localInventory = currentInventory;
        bool stopProcess = false;
        int code = AnsiConsole.Prompt(
                        new TextPrompt<int>("[green]Enter Product Code!:[/]")
                            .Validate(c =>
                            {
                                if (!localInventory.Any(p => p.ProductCode == c))
                                {
                                    AnsiConsole.MarkupLine($"\n[red]Product not Found!, do you want register it?, please, press ENTER! or DEL to return[/]");

                                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                                    while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Delete)
                                    {
                                        AnsiConsole.MarkupLine($"\n[red]Please Press Enter! or DEL to return[/]");

                                        keyInfo = Console.ReadKey(true);
                                    }
                                    if (keyInfo.Key == ConsoleKey.Delete)
                                    {
                                        stopProcess = true;
                                    }
                                }

                                    return ValidationResult.Success();

                            }));
        if (stopProcess)
        {
            return;
        }
        currentInventory = localInventory;
        Product product  = currentInventory.FirstOrDefault(p => p.ProductCode == code);
        

        if (product == null)
        {
            RegisterNewProduct(code, ref currentInventory);
            product = currentInventory.FirstOrDefault(p => p.ProductCode == code);
        }

        int quantity = AnsiConsole.Prompt(
            new TextPrompt<int>(
                $"[green]Product: {product.ProductDescription}. Enter Quantity for {(isStockIn ? "INBOUND" : "OUTBOUND")}:[/]"
                )
            );


        if (!isStockIn && product.Stock < quantity)
        {
            AnsiConsole.MarkupLine($"[red]ERROR: Insufficient estoque! Available: {product.Stock}[/]");
            Console.ReadKey();
            return;
        }

        int movedQuantity = isStockIn ? quantity : -quantity;

        product.Stock += movedQuantity;

        var newMovement = new Movement
        {
            ProductCode = code,
            Description = isStockIn ? "estoque Inbound" : "estoque Outbound",
            Quantity = movedQuantity
        };

        movementHistory.Add(newMovement);

        AnsiConsole.MarkupLine($"\n[bold green]Movement registered![/]");
        AnsiConsole.MarkupLine($"[cyan]Movement ID:[/]{newMovement.Id}");
        AnsiConsole.MarkupLine($"[bold]Final ESTOQUE:[/]{product.Stock}");
        AnsiConsole.MarkupLine($"\n[red]Press Enter to Continue![/]");
        DataReader.SaveInventory(currentInventory, "product.json");

        Console.ReadKey();
    }

    private static void RegisterNewProduct(int code, ref List<Product> currentInventory)
    {
        string name = AnsiConsole.Prompt(
            new TextPrompt<string>("[yellow]New Product Name:[/]")
                .ValidationErrorMessage("[red]Name cannot be empty.[/]")
                .Validate(n => !string.IsNullOrWhiteSpace(n))
        );

        var newProduct = new Product
        {
            ProductCode = code,
            ProductDescription = name,
            Stock = 0
        };

        currentInventory.Add(newProduct);
        AnsiConsole.MarkupLine($"\n[bold green]Product '{name}' (Code: {code}) registered with initial ESTOQUE: 0.[/]");
    }

    public static void DisplayCurrentStock(List<Product> inventory)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[yellow]>> CURRENT INVENTORY <<[/]\n");

        var table = new Table()
            .AddColumn("[bold]Code[/]")
            .AddColumn("[bold]Description[/]")
            .AddColumn("[bold]Final ESTOQUE[/]");

        foreach (var product in inventory.OrderBy(p => p.ProductCode))
        {
            table.AddRow(product.ProductCode.ToString(), product.ProductDescription, product.Stock.ToString());
        }

        AnsiConsole.Write(table);
        Console.ReadKey();
    }

    public static void DisplayMovementHistory(List<Movement> history)
    {
        AnsiConsole.Clear();
        AnsiConsole.MarkupLine("[yellow]>> MOVEMENT HISTORY <<[/]\n");

        if (!history.Any())
        {
            AnsiConsole.MarkupLine("[red]No movements recorded yet.[/]");
            Console.ReadKey();
            return;
        }

        Spectre.Console.Table table = new Spectre.Console.Table()
         .AddColumn("[bold]ID[/]")
        .AddColumn("[bold]Date[/]")
        .AddColumn("[bold]Code[/]")
        .AddColumn("[bold]Description[/]")
        .AddColumn("[bold]Qty[/]");

        foreach (var movement in history.OrderByDescending(m => m.MovementDate))
        {
            var color = movement.Quantity > 0 ? Color.Green : Color.Red;

            table.AddRow(
                new Text(movement.Id.ToString().Substring(0, 8)),
                new Text(movement.MovementDate.ToString("dd/MM HH:mm")),
                new Text(movement.ProductCode.ToString()),
                new Text(movement.Description),
                new Markup($"[{color}]{movement.Quantity}[/]")
            );
        }

        AnsiConsole.Write(table);
        Console.ReadKey();
    }
}