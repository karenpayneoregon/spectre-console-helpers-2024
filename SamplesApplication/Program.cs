using System.Runtime.InteropServices;
using SamplesApplication.Classes;
using SamplesApplication.Models;
using SamplesApplication.Validators;
using Spectre.Console.Json;
using SpectreConsoleLibrary;

using ValidationResult = FluentValidation.Results.ValidationResult;

namespace SamplesApplication;

internal partial class Program
{
    static async Task Main(string[] args)
    {
        //AnsiConsole.Record();
        await Task.Delay(0);
        //await AskQuestionExample();
        //ShowExceptionExample();
        //AddPersonExample();
        DisplayJsonExample();


        //GenericSelectionExample();

        //var html = AnsiConsole.ExportHtml();
        SpectreConsoleHelpers.ExitPrompt();
    }

    /// <summary>
    /// Adds a new person by prompting the user for their first name, last name, and birthdate.
    /// Validates the entered data and inserts the person into the database if the data is valid.
    /// </summary>
    public static void AddPersonExample()
    {

        AnsiConsole.Clear();
        SpectreConsoleHelpers.PrintHeader();

        var firstName = SpectreConsoleHelpers.FirstName("First name");
        var lastName = SpectreConsoleHelpers.LastName("Last name");
        var birthDate = SpectreConsoleHelpers.GetBirthDate("Birth date");

        Person person = new()
        {
            FirstName = firstName,
            LastName = lastName,
            BirthDate = birthDate
        };

        PersonValidator validator = new();
        ValidationResult result = validator.Validate(person);

        if (result.IsValid)
        {
            DapperOperations operations = new();
            operations.Insert(person);
            AnsiConsole.MarkupLine("[bold][green]Person added successfully.[/][/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[bold][red]Person not added.[/]");
        }

    }

    public static async Task AskQuestionExample()
    {

        AnsiConsole.Clear();
        SpectreConsoleHelpers.PrintHeader();

        if (SpectreConsoleHelpers.Question("Generate list"))
        {
            await CreateRandomData();
        }
        else
        {
            AnsiConsole.MarkupLine("[red]No data generated.[/]");
        }
    }

    private static async Task CreateRandomData()
    {
        List<Human> people = BogusOperations.People(100);

        for (var index = 0; index < 10; index++)
        {
            AnsiConsole.MarkupLine($"[cyan]Pass[/] [yellow]{index + 1}[/]");

            var items = Random.Shared.GetItems<Human>(CollectionsMarshal.AsSpan(people), 3);


            await Task.Delay(300);

            foreach (var human in items)
            {
                Console.WriteLine($"{human.Id,-5}{human.FirstName,-15}{human.LastName}");
            }

            Console.WriteLine();
        }
    }

    /// <summary>
    /// Demonstrates a generic selection example using Spectre.Console.
    /// </summary>
    /// <remarks>
    /// This method clears the console, prints a header, and then prompts the user to select products from a list.
    /// If products are selected, their names are displayed; otherwise, a message indicating no products were selected is shown.
    /// </remarks>
    public static void GenericSelectionExample()
    {

        AnsiConsole.Clear();
        SpectreConsoleHelpers.PrintHeader();

        var products = SpectreConsoleHelpers.GenericSelection(BogusOperations.Products(),"Products");
        if (products.Count >0)
        {
            foreach (var product in products)
            {
                AnsiConsole.MarkupLine($"[bold]Product:[/] [{Color.Aqua}]{product.Display}[/]");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]No products selected.[/]");
        }
    }

    /// <summary>
    /// Demonstrates handling and displaying an exception with custom formatting for showing an exception
    /// colorized.
    /// </summary>
    /// <exception cref="InvalidOperationException">
    /// Thrown when an invalid operation is attempted,
    /// with an inner exception of type <see cref="ArgumentException"/>.
    /// </exception>
    public static void ShowExceptionExample()
    {

        AnsiConsole.Clear();
        SpectreConsoleHelpers.PrintHeader();

        try
        {
    
            // Create the InvalidOperationException with the inner exception
            throw new InvalidOperationException("Operation cannot be performed", 
                new ArgumentException("Invalid argument"));
        }
        catch (Exception ex)
        {
            ex.ColorWithCyanFuchsia();
        }
    }

    static void DisplayJsonExample()
    {

        AnsiConsole.Clear();
        SpectreConsoleHelpers.PrintHeader();

        SpectreConsoleHelpers.WriteOutJson(Json);

    }

    public static string Json =>
        /*lang=json*/
        """
        [
          {
            "FirstName": "Jose",
            "LastName": "Fernandez",
            "BirthDate": "1985-01-01"
          },
          {
            "FirstName": "Miguel",
            "LastName": "Lopez",
            "BirthDate": "1970-12-04"
          },
          {
            "FirstName": "Angel",
            "LastName": "Perez",
            "BirthDate": "1980-09-11"
          }
        ]
        """;

}