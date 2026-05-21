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
            CustomConsole.WriteLine($"Problem Slug: {plan.ProblemSlug}", new MessageType("info"));
        }

        if (!CopyTo(plan.TemplateDirectory, plan.OutputDirectory))
        {
            CustomConsole.WriteLine("Failed to copy template files to output directory.", new MessageType("error"));
            return;
        }
    }

    private bool CopyTo(string sourcePath, string destinationPath)
    {
        try
        {
            var sourceDirectory = new DirectoryInfo(sourcePath);
            if (!sourceDirectory.Exists)
            {
                CustomConsole.WriteLine($"Source directory does not exist: {sourceDirectory.FullName}", new MessageType("error"));
                return false;
            }

            Directory.CreateDirectory(destinationPath);

            foreach (FileInfo file in sourceDirectory.GetFiles())
            {
                string destinationFilePath = Path.Combine(destinationPath, file.Name);
                file.CopyTo(destinationFilePath, true);
            }

            foreach (DirectoryInfo subdirectory in sourceDirectory.GetDirectories())
            {
                string destinationSubdirectory = Path.Combine(destinationPath, subdirectory.Name);
                if (!CopyTo(subdirectory.FullName, destinationSubdirectory))
                {
                    return false;
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            CustomConsole.WriteLine($"Error copying directory from {sourcePath} to {destinationPath}: {ex.Message}", new MessageType("error"));
            return false;
        }
    }
}
