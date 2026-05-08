namespace LeetGen.Core;

public class Application(ApplicationOptions options, ILanguageHandler? languageHandler = null)
{
    public ApplicationOptions Options { get; set; } = options;
    public ILanguageHandler? LanguageHandler { get; set; } = languageHandler;

    public async Task RunAsync()
    {
        // Fetch problem details from the leetcode HTTP API
        // Create an object of that fetch result, which contains the problem title, description, etc.

        // GenerationPlanBuilder planBuilder = new(Options, LanguageHandler, LeetCodeDetails);
        // GenerationPlan plan = planBuilder.BuildPlan();
    }
}
