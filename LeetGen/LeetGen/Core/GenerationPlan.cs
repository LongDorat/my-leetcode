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
        StringBuilder outputPath = new();
        outputPath.Append(_options.OutputDirectory);
        outputPath.Append(Path.DirectorySeparatorChar);
        outputPath.Append(LanguageHandler.LanguageSlug);
        outputPath.Append(Path.DirectorySeparatorChar);
        outputPath.Append($"{_options.ProblemNumber:D4}_{ProblemSlug}");
        OutputDirectory = outputPath.ToString();

        StringBuilder templatePath = new();
        templatePath.Append(_options.TemplateDirectory);
        templatePath.Append(Path.DirectorySeparatorChar);
        templatePath.Append(LanguageHandler.LanguageSlug);
        TemplateDirectory = templatePath.ToString();
    }
}
