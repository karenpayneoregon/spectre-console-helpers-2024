using SamplesApplication.Classes;
using SpectreConsoleLibrary;

namespace SamplesApplication;

internal partial class Program
{
    static void Main(string[] args)
    {
        //AnsiConsole.Record();

        var products = SpectreConsoleHelpers.GenericSelection(BogusOperations.Products());
        if (products.Count >0)
        {
            foreach (var product in products)
            {
                AnsiConsole.MarkupLine($"[bold]Product Name:[/] [{Color.Aqua}]{product.ProductName}[/]");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]No products selected.[/]");
        }

        SpectreConsoleHelpers.ExitPrompt();
    }
}