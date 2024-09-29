﻿using System.Runtime.CompilerServices;
using SpectreConsoleLibrary.Extensions;

#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace SpectreConsoleLibrary;

public partial class SpectreConsoleHelpers
{
    public static void ExitPrompt()
    {
        Console.WriteLine();

        Render(new Rule($"[yellow]Press[/] [cyan]ENTER[/] [yellow]to exit the demo[/]")
            .RuleStyle(Style.Parse("silver")).LeftJustified());

        Console.ReadLine();
    }

    private static void Render(Rule rule)
    {
        AnsiConsole.Write(rule);
        AnsiConsole.WriteLine();
    }

    public static void LineSeparator()
    {
        AnsiConsole.Write(new Rule().RuleStyle(Style.Parse("grey")).Centered());
        Console.WriteLine();
    }
    public static void PrintHeader([CallerMemberName] string? methodName = null)
    {
        AnsiConsole.MarkupLine($"[yellow]{methodName!.SplitCase()}[/]");
        Console.WriteLine();
    }

    public static string PrintValue<T>(T item) => $"[cyan]{item.ToString()}[/]";
    public static string PrintYes<T>(T item) => $"[green]{item.ToString()}[/]";
    public static string PrintNo<T>(T item) => $"[red]{item.ToString()}[/]";


}
