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
            Console.WriteLine("Debug mode enabled.");
            Console.WriteLine($"Output Directory: {options.OutputDirectory}");
            Console.WriteLine($"Template Directory: {options.TemplateDirectory}");
            Console.WriteLine($"Language: {options.Language}");
        }

        var services = new ServiceCollection();
        services.AddSingleton<ILanguageHandler, CSharpHandler>();
        // Add other language handlers here as needed

        var serviceProvider = services.BuildServiceProvider();
        var languageHandlers = serviceProvider.GetServices<ILanguageHandler>();
        if (languageHandlers == null || !languageHandlers.Any())
        {
            Console.WriteLine("No language handlers are registered.");
            return;
        }
        var languageHandler = languageHandlers?.FirstOrDefault(handler => string.Equals(handler.LanguageSlug, options.Language, StringComparison.OrdinalIgnoreCase));

        if (languageHandler == null)
        {
            Console.WriteLine($"The language is unsupported: '{options.Language}'.");
            Console.WriteLine("Supported languages:");
            foreach (var handler in languageHandlers)
            {
                Console.WriteLine($"- {handler.LanguageSlug}");
            }
            return;
        }
    }
}
