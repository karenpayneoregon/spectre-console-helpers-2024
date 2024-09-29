﻿namespace SpectreConsoleLibrary.Extensions;
public static class StringExtensions
{
    /// <summary>
    /// Splits a camel case or Pascal case string into separate words.
    /// </summary>
    /// <param name="input">The input string to be split.</param>
    /// <returns>A string with spaces inserted between words.</returns>
    public static string SplitCase(this string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        Span<char> result = stackalloc char[input.Length * 2];
        var resultIndex = 0;

        for (var index = 0; index < input.Length; index++)
        {
            var currentChar = input[index];

            if (index > 0 && char.IsUpper(currentChar))
            {
                result[resultIndex++] = ' ';
            }

            result[resultIndex++] = currentChar;
        }

        return result[..resultIndex].ToString();

    }
}
