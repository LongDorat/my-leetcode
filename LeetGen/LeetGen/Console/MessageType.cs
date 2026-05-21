namespace LeetGen.Console;

public class MessageType(string type)
{
    public string Type { get; private set; } = type.ToLower() switch
    {
        "info" => "[INFO]\t",
        "warning" => "[WARN]\t",
        "error" => "[ERROR]\t",
        "success" => "[GOOD]\t",
        _ => throw new ArgumentException("Invalid message type"),
    };

    public ConsoleColor Color => type.ToLower() switch
    {
        "info" => ConsoleColor.Green,
        "warning" => ConsoleColor.Yellow,
        "error" => ConsoleColor.Red,
        "success" => ConsoleColor.Cyan,
        _ => ConsoleColor.White,
    };
}
