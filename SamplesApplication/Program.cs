using SamplesApplication.Classes;
using SpectreConsoleLibrary;

namespace SamplesApplication;

internal partial class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.Record();
        AnsiConsole.MarkupLine("[yellow]Hello[/]");

        var test = SpectreConsoleHelpers.GenericSelection(BogusOperations.Products());
        foreach (var product in test)
        {
            Console.WriteLine(product.ProductName);
        }
        Console.ReadLine();
    }
}