namespace SpectreConsoleLibrary;

public partial class SpectreConsoleHelpers
{
    /// <summary>
    /// Displays a question prompt and returns the user's response.
    /// </summary>
    /// <param name="questionText">The text of the question.</param>
    /// <returns>True if the user selects 'Y', False if the user selects 'N'.</returns>
    public static bool Question(string questionText)
    {
        ConfirmationPrompt prompt = new($"[{Color.Yellow}]{questionText}[/]")
        {
            DefaultValueStyle = new(Color.Cyan1, Color.Black, Decoration.None),
            ChoicesStyle = new(Color.Yellow, Color.Black, Decoration.None),
            InvalidChoiceMessage = $"[{Color.Red}]Invalid choice[/]. Please select a Y or N.",
        };

        return prompt.Show(AnsiConsole.Console);
    }

    /// <summary>
    /// Displays a question prompt with red background and white text foreground, and returns the user's response.
    /// </summary>
    /// <param name="questionText">The text of the question.</param>
    /// <returns>True if the user selects '1', False if the user selects '2'.</returns>
    public static bool QuestionRed(string questionText)
    {
        ConfirmationPrompt prompt = new($"[{Color.White} on {Color.Red}]{questionText}[/]")
        {
            DefaultValueStyle = new(Color.White, Color.Red, Decoration.None),
            ChoicesStyle = new(Color.White, Color.Red, Decoration.None),
            InvalidChoiceMessage = $"[{Color.Red}]Invalid choice[/]. Please select a 1 or 2.",
            Yes = '1',
            No = '2',
        };

        return prompt.Show(AnsiConsole.Console);
    }

    /// <summary>
    /// Displays a question prompt with specified foreground and background colors, and returns the user's response.
    /// </summary>
    /// <param name="questionText">The text of the question.</param>
    /// <param name="foreGround">The foreground color</param>
    /// <param name="backGround">The background color</param>
    /// <returns>True if the user selects 'Y', False if the user selects 'N'.</returns>
    public static bool Question(string questionText, Color foreGround, Color backGround)
    {
        ConfirmationPrompt prompt = new($"[{foreGround} on {backGround}]{questionText}[/]")
        {
            DefaultValueStyle = new(foreGround, backGround, Decoration.None),
            ChoicesStyle = new(foreGround, backGround, Decoration.None),
            InvalidChoiceMessage = $"[{Color.Red}]Invalid choice[/]. Please select a Y or N."
        };

        return prompt.Show(AnsiConsole.Console);
    }
}
