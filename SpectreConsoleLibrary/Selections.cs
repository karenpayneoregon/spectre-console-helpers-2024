namespace SpectreConsoleLibrary;
public partial class SpectreConsoleHelpers
{
    /// <summary>
    /// Prompts the user to select one or more months from a list.
    /// </summary>
    /// <param name="pageSize">The number of months to display per page in the selection prompt.</param>
    /// <param name="title">The title of the selection prompt. Defaults to "Months".</param>
    /// <returns>A list of selected month names as strings.</returns>
    public static List<string> MonthsSelection(int pageSize, string title = "Months") 
        => AnsiConsole.Prompt
    (
        new MultiSelectionPrompt<string>()
            .PageSize(pageSize)
            .Required(false)
            .Title($"[{_promptColor}]{title}[/]?")
            .InstructionsText("[grey](Press [yellow]<space>[/] to toggle a month, [yellow]<enter>[/] to accept)[/] or [red]Enter[/] w/o any selections to cancel")
            .AddChoices(CurrentInfo!.MonthNames[..^1])
            .HighlightStyle(new Style(Color.White, Color.Black, Decoration.Invert))
    );


    /// <summary>
    /// Prompts the user to select one or more items from a provided list.
    /// </summary>
    /// <typeparam name="T">The type of the items in the list. Must be non-nullable.</typeparam>
    /// <param name="values">The list of items to select from.</param>
    /// <param name="title">The title of the selection prompt. Defaults to "Generic".</param>
    /// <returns>A list of selected items of type <typeparamref name="T"/>.</returns>
    /// <remarks>
    /// Colors are hard-coded for demonstration purposes.
    /// </remarks>
    public static List<T> GenericSelection<T>(List<T> values, string title = "Generic") where T : notnull => AnsiConsole.Prompt
        (
            prompt: new MultiSelectionPrompt<T>()
                .PageSize(10)
                .Required(false)
                .Title($"[{_promptColor}]{title}[/]?")
                .InstructionsText("[grey](Press [yellow]<space>[/] to toggle an item, " +
                                  "[yellow]<enter>[/] to accept)[/] or [red]Enter[/] w/o " +
                                  "any selections to cancel")
                .AddChoices([.. values])
                .HighlightStyle(new Style(Color.Blue, Color.White, Decoration.Invert))
        );
}
