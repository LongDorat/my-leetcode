using Microsoft.Extensions.DependencyInjection;

namespace LeetGen;

class Program
{
    static void Main(string[] args)
    {
        CommandLineParser parser = new();
        ApplicationOptions options = parser.Parse(args);

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

        if (!ValidateOptions(ref options, languageHandler, languageHandlers))
        {
            return;
        }
        if (options.isDebug)
        {
            CustomConsole.WriteLine("Options validated successfully to debug mode.", new MessageType("info"));
            CustomConsole.WriteLine("Options after validation:", new MessageType("info"));
            System.Console.WriteLine($"\t\tOutput Directory: {options.OutputDirectory}");
            System.Console.WriteLine($"\t\tTemplate Directory: {options.TemplateDirectory}");
            System.Console.WriteLine($"\t\tLanguage: {options.Language}");
        }
    }

    private static bool ValidateOptions(ref ApplicationOptions options, ILanguageHandler? currentHandler = null, IEnumerable<ILanguageHandler>? allHandlers = null)
    {
        if (options.isDebug)
        {
            options.ProblemNumber = 1;
            options.Language = "csharp";
        }

        bool isSuccess = true;
        if (!Directory.Exists(options.OutputDirectory))
        {
            CustomConsole.WriteLine($"Output directory does not exist: {options.OutputDirectory}", new MessageType("error"));
            isSuccess = false;
        }
        if (!Directory.Exists(options.TemplateDirectory))
        {
            CustomConsole.WriteLine($"Template directory does not exist: {options.TemplateDirectory}", new MessageType("error"));
            isSuccess = false;
        }

        if (options.ProblemNumber <= 0)
        {
            CustomConsole.WriteLine($"Invalid problem number: {options.ProblemNumber}. Problem number must be a positive integer.", new MessageType("error"));
            isSuccess = false;
        }

        if (allHandlers == null)
        {
            CustomConsole.WriteLine("Language handler validation failed due to no available handlers.", new MessageType("error"));
            isSuccess = false;
        }
        if (currentHandler == null && allHandlers != null /* Just to satisfy the compiler warning */)
        {
            CustomConsole.WriteLine($"Unsupported language: {options.Language}", new MessageType("error"));
            var supportedLanguages = allHandlers.Select(h => h.LanguageSlug).ToArray();
            CustomConsole.WriteOptions("Supported languages are:", new MessageType("info"), supportedLanguages);
            isSuccess = false;
        }
        return isSuccess;
    }
}
