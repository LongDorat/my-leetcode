namespace LeetGen.Core;

public class Application(ApplicationOptions options, ILanguageHandler? languageHandler = null)
{
    public ApplicationOptions Options { get; set; } = options;
    public ILanguageHandler? LanguageHandler { get; set; } = languageHandler;
    public IProblemDetailsAPI? LeetCodeDetails { get; set; }

    public async Task RunAsync()
    {
        if (Options.isDebug)
        {
            LeetCodeDetails = new MockProblemDetails();
        }
        else
        {
            CustomConsole.WriteLine("No implementation for fetching problem details yet. Please use --debug for now.", new MessageType("error"));
        }

        GenerationPlan plan = new(Options, LanguageHandler!, LeetCodeDetails!);
        plan.Build();
        if (Options.isDebug)
        {
            CustomConsole.WriteLine($"Output Directory: {plan.OutputDirectory}", new MessageType("info"));
            CustomConsole.WriteLine($"Template Directory: {plan.TemplateDirectory}", new MessageType("info"));
            CustomConsole.WriteLine($"Language: {plan.LanguageHandler.LanguageSlug}", new MessageType("info"));
            CustomConsole.WriteLine($"Problem Number: {plan.ProblemNumber}", new MessageType("info"));
        }
    }
}
