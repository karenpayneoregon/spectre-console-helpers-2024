using Spectre.Console.Json;

namespace SpectreConsoleLibrary;
public partial class SpectreConsoleHelpers
{
    public static void WriteOutJson(string json)
    {
        AnsiConsole.Write(
            new JsonText(json)
                .BracesColor(Color.Red)
                .BracketColor(Color.Green)
                .ColonColor(Color.Blue)
                .CommaColor(Color.Red)
                .StringColor(Color.Green)
                .NumberColor(Color.Blue)
                .BooleanColor(Color.Red)
                .MemberColor(Color.Wheat1)
                .NullColor(Color.Green));
    }
}
