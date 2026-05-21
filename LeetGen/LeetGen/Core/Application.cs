namespace LeetGen.Core;

public class Application
{
    public ApplicationOptions Options { get; set; }
    public ILanguageHandler? LanguageHandler { get; set; }
    public IProblemDetailsAPI? LeetCodeDetails { get; set; }

    private readonly GenerationPlan _plan;

    public Application(ApplicationOptions options, ILanguageHandler? languageHandler = null)
    {
        Options = options;
        LanguageHandler = languageHandler;

        if (Options.isDebug)
        {
            LeetCodeDetails = new MockProblemDetails();
        }
        else
        {
            CustomConsole.WriteLine("No implementation for fetching problem details yet. Please use --debug for now.", new MessageType("error"));
        }

        _plan = new GenerationPlan(Options, LanguageHandler!, LeetCodeDetails!);
        _plan.Build();

        if (Options.isDebug)
        {
            CustomConsole.WriteLine($"Output Directory: {_plan.OutputDirectory}", new MessageType("info"));
            CustomConsole.WriteLine($"Template Directory: {_plan.TemplateDirectory}", new MessageType("info"));
            CustomConsole.WriteLine($"Language: {_plan.LanguageHandler.LanguageSlug}", new MessageType("info"));
            CustomConsole.WriteLine($"Problem Number: {_plan.ProblemNumber}", new MessageType("info"));
            CustomConsole.WriteLine($"Problem Slug: {_plan.ProblemSlug}", new MessageType("info"));
        }
    }

    public async Task GenerateAsync()
    {
        if (!CopyTo(_plan.TemplateDirectory, _plan.OutputDirectory))
        {
            CustomConsole.WriteLine("Failed to copy template files to output directory.", new MessageType("error"));
            return;
        }

        if (LanguageHandler != null && !LanguageHandler.ReplacePlaceHolders(_plan.OutputDirectory))
        {
            CustomConsole.WriteLine("Failed to replace placeholders in the output directory.", new MessageType("error"));
            return;
        }

        if (LanguageHandler != null && !LanguageHandler.Initialize(_plan.OutputDirectory))
        {
            CustomConsole.WriteLine("Failed to initialize language handler.", new MessageType("error"));
            return;
        }

        CustomConsole.WriteLine("Generation completed successfully!", new MessageType("success"));
    }

    public async Task RemoveAsync()
    {
        string targetPath = _plan.OutputDirectory;
        if (!Directory.Exists(targetPath))
        {
            CustomConsole.WriteLine($"Output directory does not exist: {targetPath}", new MessageType("error"));
            return;
        }

        CustomConsole.WriteLine($"This will delete: {targetPath}", new MessageType("warning"));
        System.Console.Write("Type 'yes' to confirm: ");
        string? confirmation = System.Console.ReadLine();
        if (!string.Equals(confirmation, "yes", StringComparison.OrdinalIgnoreCase))
        {
            CustomConsole.WriteLine("Remove cancelled.", new MessageType("info"));
            return;
        }

        try
        {
            Directory.Delete(targetPath, true);
            CustomConsole.WriteLine("Remove completed successfully!", new MessageType("success"));
        }
        catch (Exception ex)
        {
            CustomConsole.WriteLine($"Failed to remove output directory: {ex.Message}", new MessageType("error"));
        }

        await Task.CompletedTask;
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
