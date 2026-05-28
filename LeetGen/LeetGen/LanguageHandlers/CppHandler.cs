using System.Diagnostics;

namespace LeetGen.LanguageHandlers;

public class CppHandler : ILanguageHandler
{
    public string LanguageSlug => "cpp";

    public bool ReplacePlaceHolders(GenerationPlan plan)
    {
        return true;
    }

    public bool Initialize(GenerationPlan plan)
    {
        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "bash",
                Arguments = "Run.sh",
                WorkingDirectory = plan.OutputDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(startInfo);
            if (process == null)
            {
                CustomConsole.WriteLine("Failed to start initialization script for C++ environment.", new MessageType("error"));
                return false;
            }

            process.WaitForExit();
            return process.ExitCode == 0;
        }
        catch
        {
            CustomConsole.WriteLine("An error occurred while initializing the C++ environment.", new MessageType("error"));
            return false;
        }
    }
}