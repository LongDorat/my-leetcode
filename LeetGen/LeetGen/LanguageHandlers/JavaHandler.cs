using System.Diagnostics;

namespace LeetGen.LanguageHandlers;

class JavaHandler : ILanguageHandler
{
    public string LanguageSlug => "java";

    public bool ReplacePlaceHolders(GenerationPlan plan)
    {
        // Implement Java-specific placeholder replacement logic here
        return true;
    }

    public bool Initialize(GenerationPlan plan)
    {
        try
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "mvn",
                Arguments = "test",
                WorkingDirectory = plan.OutputDirectory,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(startInfo);
            if (process == null)
            {
                CustomConsole.WriteLine("Failed to start Maven process for Java initialization.", new MessageType("error"));
                return false;
            }

            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                string errorOutput = process.StandardError.ReadToEnd();
                CustomConsole.WriteLine($"Maven process exited with code {process.ExitCode}. Error output: {errorOutput}", new MessageType("error"));
                return false;
            }
        }
        catch (Exception ex)
        {
            CustomConsole.WriteLine($"Error occurred while initializing Java environment: {ex.Message}", new MessageType("error"));
            return false;
        }
        return true;
    }
}