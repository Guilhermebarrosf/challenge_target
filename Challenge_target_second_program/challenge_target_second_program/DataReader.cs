using System.Text.Json;
using System.IO;
using System.Text;
using Spectre.Console;
using System.Collections.Generic;
using System.Linq;

public static class DataReader
{
    public static bool loadInitialInventory(ref List<Product> currentInventory)
    {
        string filePath = "product.json";

        if (!File.Exists(filePath) || string.IsNullOrWhiteSpace(File.ReadAllText(filePath)))
        {
            string initialJson = @"{""estoque"":[]}";
            File.WriteAllText(filePath, initialJson);
        }

        try
        {
            string json = File.ReadAllText(filePath);
            Root? root = JsonSerializer.Deserialize<Root>(json);
            if(root?.Inventory == null) return false;
            
            currentInventory = root.Inventory;
            return true;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error loading JSON: {ex.Message}[/]");
            return false;
        }
    }

    public static void SaveInventory(List<Product> inventory, string filePath)
    {
        var rootData = new Root { Inventory = inventory };
        var options = new JsonSerializerOptions { WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(rootData, options);

        try
        {
            File.WriteAllText(filePath, jsonString, Encoding.UTF8);
            AnsiConsole.MarkupLine($"[green]Data saved successfully to {filePath}[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]ERROR saving file: {ex.Message}[/]");
        }
    }
}