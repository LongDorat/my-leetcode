using System;

namespace LeetGen;

public class MessageType(string type)
{
    public string Type { get; private set; } = type.ToLower() switch
    {
        "info" => "[INFO]\t",
        "warning" => "[WARN]\t",
        "error" => "[ERROR]\t",
        _ => throw new ArgumentException("Invalid message type"),
    };

    public ConsoleColor Color => type.ToLower() switch
    {
        "info" => ConsoleColor.Green,
        "warning" => ConsoleColor.Yellow,
        "error" => ConsoleColor.Red,
        _ => ConsoleColor.White,
    };
}
