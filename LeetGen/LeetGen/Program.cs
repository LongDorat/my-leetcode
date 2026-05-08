namespace LeetGen;

class Program
{
    static void Main(string[] args)
    {
        CommandLineParser parser = new();
        ApplicationOptions options = parser.Parse(args);

        if (options.isDebug)
        {
            Console.WriteLine("Debug mode enabled.");
            Console.WriteLine($"Output Directory: {options.OutputDirectory}");
            Console.WriteLine($"Template Directory: {options.TemplateDirectory}");
            Console.WriteLine($"Language: {options.Language}");
        }
    }
}
