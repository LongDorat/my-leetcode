using System.Text;

namespace LeetGen.Core;

public class GenerationPlan
{
    public string OutputDirectory { get; set; } = "";
    public string TemplateDirectory { get; set; } = "";
    public ILanguageHandler LanguageHandler { get; set; }
    public int ProblemNumber { get; set; }
    public string ProblemSlug { get; set; }

    private ApplicationOptions _options;

    public GenerationPlan(ApplicationOptions options, ILanguageHandler languageHandler, IProblemDetailsAPI problemDetails)
    {
        _options = options;
        LanguageHandler = languageHandler;
        ProblemNumber = options.ProblemNumber;
        ProblemSlug = problemDetails.Slug;
    }

    public void Build()
    {
        OutputDirectory = Path.Combine(_options.OutputDirectory, LanguageHandler.LanguageSlug, $"{_options.ProblemNumber:D4}_{ProblemSlug}");

        TemplateDirectory = Path.Combine(_options.TemplateDirectory, LanguageHandler.LanguageSlug);
    }
}
