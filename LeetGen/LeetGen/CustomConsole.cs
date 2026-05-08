using System;

namespace LeetGen;

public static class CustomConsole
{
    public static void WriteLine(string message, MessageType type, bool? isNewlineBefore = false)
    {
        if (isNewlineBefore == true)
        {
            Console.WriteLine();
        }
        Console.ForegroundColor = type.Color;
        Console.WriteLine($"{type.Type}: {message}");
        Console.ResetColor();
    }

    public static void WriteOptions(string message, MessageType type, string[] options)
    {
        Console.ForegroundColor = type.Color;
        Console.WriteLine($"{type.Type}: {message}");
        foreach (var option in options)
        {
            Console.WriteLine($"\t\t- {option}");
        }
        Console.ResetColor();
    }
}
