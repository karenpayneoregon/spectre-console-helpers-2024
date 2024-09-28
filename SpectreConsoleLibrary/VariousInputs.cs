using Easy_Password_Validator;
using Easy_Password_Validator.Models;

namespace SpectreConsoleLibrary;
public partial class SpectreConsoleHelpers
{
    private static Color _promptColor = Color.White;
    private static Color _promptStyle = Color.CadetBlue;

    private static Color _errorForeGround = Color.White;
    private static Color _errorBackGround = Color.Red;


    /// <summary>
    /// Prompts the user to enter their first name with an optional custom prompt text.
    /// </summary>
    /// <param name="text">The custom text to display in the prompt. Defaults to "First name".</param>
    /// <returns>The entered first name as a string. The input can be empty.</returns>
    public static string GetFirstName(string text = "First name") =>
        AnsiConsole.Prompt(
            new TextPrompt<string>($"[{_promptColor}]{text}[/]")
                .PromptStyle(_promptStyle)
                .AllowEmpty()
        );


    /// <summary>
    /// Prompts the user to enter their first name with an optional custom prompt text.
    /// </summary>
    /// <param name="text">The custom text to display in the prompt. Defaults to "First name".</param>
    /// <returns>The entered first name as a string. The input must have at least three characters.</returns>
    public static string FirstName(string text = "First name") =>
        AnsiConsole.Prompt(
            new TextPrompt<string>($"[{_promptColor}]{text}[/]")
                .PromptStyle(_promptStyle)
                .Validate(value => value.Length switch
                {
                    < 3 => ValidationResult.Error("[red]Must have at least three characters[/]"),
                    _ => ValidationResult.Success(),
                })
                .ValidationErrorMessage($"[{_errorForeGround} on {_errorBackGround}]Please enter your first name[/]"));
    
    /// <summary>
    /// Prompts the user to enter their last name with an optional custom prompt text.
    /// </summary>
    /// <param name="text">The custom text to display in the prompt. Defaults to "Last name".</param>
    /// <returns>The entered last name as a string. The input can be empty.</returns>
    public static string GetLastName(string text = "Last name") =>
        AnsiConsole.Prompt(
            new TextPrompt<string>($"[{_promptColor}]{text}[/]")
                .PromptStyle(_promptStyle)
                .AllowEmpty()
        );

    /// <summary>
    /// Prompts the user to enter their last name with an optional custom prompt text.
    /// </summary>
    /// <param name="text">The custom text to display in the prompt. Defaults to "Last name".</param>
    /// <returns>The entered last name as a string. The input can be empty.</returns>
    public static string LastName(string text = "Last name") =>
        AnsiConsole.Prompt(
            new TextPrompt<string>($"[{_promptColor}]{text}[/]?")
                .PromptStyle(_promptStyle)
                .ValidationErrorMessage($"[{_errorForeGround} on {_errorBackGround}]Please enter your last name[/]"));

    
    /// <summary>
    /// Prompts the user to enter their birthdate.
    /// </summary>
    /// <param name="title">The title of the prompt displayed to the user. Defaults to "Enter your birthdate".</param>
    /// <returns>
    /// The entered birthdate as a <see cref="DateOnly"/> object, or <c>null</c> if no date is entered.
    /// The year must be greater than 1900.
    /// </returns>
    public static DateOnly? GetBirthDate(string title = "Enter your birth date")
    {
        /*
         * doubtful there is a birthday for the current person
         * but if checking say a parent or grandparent this will not allow before 1900
         */
        const int minYear = 1900;

        return AnsiConsole.Prompt(
            new TextPrompt<DateOnly>($"[{_promptColor}]{title}[/]:")
                .PromptStyle(_promptStyle)
                .ValidationErrorMessage($"[{_errorForeGround} on {_errorBackGround}]Please enter a valid date or press ENTER to not enter a date[/]")
                .Validate(dt => dt.Year switch
                {
                    <= minYear => ValidationResult.Error($"[{_errorForeGround} on {_errorBackGround}]Year must be greater than {minYear}[/]"),
                    _ => ValidationResult.Success(),
                })
                .AllowEmpty());
    }


    /// <summary>
    /// Prompts the user to enter a time value.
    /// </summary>
    /// <param name="defaultValue">The default time value to display in the prompt. The default is "00:00:00".</param>
    /// <returns>
    /// The entered time as a <see cref="TimeOnly"/> object if the input is valid; otherwise, <c>null</c>.
    /// </returns>
    public static TimeOnly? GetTimeOnly(string defaultValue = "00:00:00")
    {
        var inout = AnsiConsole.Prompt(new TextPrompt<string>($"[{_promptColor}]Time[/]:")
            .PromptStyle(_promptStyle)
            .DefaultValue(defaultValue)
            .AllowEmpty());

        return TimeOnly.TryParse(inout, out var time) ? time : null;
    }

    
    /// <summary>
    /// Prompts the user to enter a new password.
    /// </summary>
    /// <param name="title">The title of the prompt. Defaults to "Password".</param>
    /// <returns>The entered password as a string.</returns>
    /// <remarks>
    /// The password input is hidden from display and validated against predefined rules.
    /// </remarks>
    public static string GetNewPassword(string title = "Password") =>
        AnsiConsole.Prompt(
            new TextPrompt<string>($"[{_promptColor}]{title}[/]?")
                .PromptStyle(_promptStyle)
                .Secret()
                .Validate(ValidatePassword)
                .ValidationErrorMessage($"[{_errorForeGround} on {_errorBackGround}]Entry does not match rules for creating a new password[/]"));


    /// <summary>
    /// Validates the provided password against predefined rules.
    /// </summary>
    /// <param name="password">The password to validate.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating whether the password meets the required criteria.
    /// </returns>
    private static ValidationResult ValidatePassword(string password)
    {

        var passwordValidator = new PasswordValidatorService(new PasswordRequirements());
        bool isValid;
   
        try
        {
            isValid = passwordValidator.TestAndScore(password);
        }
        catch (Exception ex)
        {
            isValid = false;
        }


        return isValid ? ValidationResult.Success() :
            ValidationResult.Error("Does not match rules for creating a password");
    }

    /// <summary>
    /// Prompts the user to enter a password.
    /// </summary>
    /// <param name="text">The prompt text to display to the user. Defaults to "Enter a password".</param>
    /// <returns>The entered password as a string.</returns>
    public static string GetPassword(string text = "Enter a password")
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>($"[{_promptColor}]{text}[/]?")
                .PromptStyle("grey50")
                .Secret());
    }


    /// <summary>
    /// Prompts the user to enter an integer value.
    /// </summary>
    /// <param name="text">The prompt message to display to the user.</param>
    /// <returns>The entered integer value.</returns>
    public static int GetInt(string text) =>
        AnsiConsole.Prompt(
            new TextPrompt<int>($"[{_promptColor}]{text}[/]")
                .PromptStyle(_promptStyle)
                .DefaultValue(1)
                .DefaultValueStyle(new(_promptColor, Color.Black, Decoration.None)));

    /// <summary>
    /// Prompts the user to enter a value of type <typeparamref name="T"/> with a specified prompt message and default value.
    /// </summary>
    /// <typeparam name="T">The type of the value to be entered.</typeparam>
    /// <param name="prompt">The message to display to the user.</param>
    /// <param name="defaultValue">The default value to use if the user does not provide an input.</param>
    /// <returns>The entered value of type <typeparamref name="T"/>.</returns>
    public static T Get<T>(string prompt, T defaultValue) =>
        AnsiConsole.Prompt(
            new TextPrompt<T>($"[{_promptColor}]{prompt}[/]")
                .PromptStyle(_promptStyle)
                .DefaultValueStyle(new(_promptStyle))
                .DefaultValue(defaultValue)
                .ValidationErrorMessage($"[{_errorForeGround} on {_errorBackGround}]Invalid entry![/]"));
}
