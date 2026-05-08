using Microsoft.Extensions.DependencyInjection;
using LeetGen.LanguageHandlers;

namespace LeetGen;

class Program
{
    static void Main(string[] args)
    {
        CommandLineParser parser = new();
        ApplicationOptions options = parser.Parse(args);

        if (options.isDebug)
        {
            CustomConsole.WriteLine("Debug mode is enabled.", new MessageType("info"));
            Console.WriteLine($"\t\tOutput Directory: {options.OutputDirectory}");
            Console.WriteLine($"\t\tTemplate Directory: {options.TemplateDirectory}");
            Console.WriteLine($"\t\tLanguage: {options.Language}");
        }

        var services = new ServiceCollection();
        services.AddSingleton<ILanguageHandler, CSharpHandler>();
        // Add other language handlers here as needed

        var serviceProvider = services.BuildServiceProvider();
        var languageHandlers = serviceProvider.GetServices<ILanguageHandler>();
        if (languageHandlers == null || !languageHandlers.Any())
        {
            CustomConsole.WriteLine("No language handlers are registered.", new MessageType("error"));
            return;
        }
        var languageHandler = languageHandlers?.FirstOrDefault(handler => string.Equals(handler.LanguageSlug, options.Language, StringComparison.OrdinalIgnoreCase));

        if (languageHandler == null && languageHandlers != null /*Just to satisfy the nullability check*/)
        {
            CustomConsole.WriteLine($"The language is unsupported: '{options.Language}'.", new MessageType("error"));
            CustomConsole.WriteOptions("Supported languages:", new MessageType("info"), languageHandlers.Select(h => h.LanguageSlug).ToArray());
            return;
        }
    }
}
