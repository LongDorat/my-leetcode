namespace LeetGen.Core;

public class GenerationPlan(string outputDirectory, string templateDirectory, ILanguageHandler languageHandler, int problemNumber, string problemSlug)
{
    public string OutputDirectory { get; set; } = outputDirectory;
    public string TemplateDirectory { get; set; } = templateDirectory;
    public ILanguageHandler LanguageHandler { get; set; } = languageHandler;
    public int ProblemNumber { get; set; } = problemNumber;
    public string ProblemSlug { get; set; } = problemSlug;
}
