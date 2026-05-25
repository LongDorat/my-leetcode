namespace LeetGen.Console;

public static class CustomConsole
{
    public static void WriteLine(string message, MessageType type, bool? isNewlineBefore = false)
    {
        if (isNewlineBefore == true)
        {
            System.Console.WriteLine();
        }
        System.Console.ForegroundColor = type.Color;
        System.Console.WriteLine($"{type.Type}: {message}");
        System.Console.ResetColor();
    }

    public static void WriteOptions(string message, MessageType type, string[] options)
    {
        System.Console.ForegroundColor = type.Color;
        System.Console.WriteLine($"{type.Type}: {message}");
        foreach (var option in options)
        {
            System.Console.WriteLine($"\t\t- {option}");
        }
        System.Console.ResetColor();
    }
}
