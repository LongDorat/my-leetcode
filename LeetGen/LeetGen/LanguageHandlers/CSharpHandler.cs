using System.Diagnostics;

namespace LeetGen.LanguageHandlers;

public class CSharpHandler : ILanguageHandler
{
    public string LanguageSlug => "csharp";

    public bool ReplacePlaceHolders(GenerationPlan plan)
    {
        CustomConsole.WriteLine(".NET project doesn't have any placeholders to replace, skipping this step.", new MessageType("info"));
        return true;
    }

    public bool Initialize(GenerationPlan plan)
    {
        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "restore",
                WorkingDirectory = plan.OutputDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(startInfo);
            if (process == null)
            {
                return false;
            }

            process.WaitForExit();
            return process.ExitCode == 0;
        }
        catch
        {
            return false;
        }
    }
}
