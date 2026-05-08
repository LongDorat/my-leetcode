using System;
using System.CommandLine;

namespace LeetGen;

public class CommandLineParser
{

    public ApplicationOptions Parse(string[] args)
    {
        Option<string> outputDirectoryOption = new("--output")
        {
            Description = "The directory where generated files will be saved.",
            DefaultValueFactory = _ => "./debug-output"
        };
        Option<string> templateDirectoryOption = new("--template")
        {
            Description = "The directory where template files are located.",
            DefaultValueFactory = _ => "./debug-templates"
        };
        Option<int> problemNumberOption = new("--problem")
        {
            Description = "The LeetCode problem number to generate code for.",
            DefaultValueFactory = _ => 0
        };
        Option<string> languageOption = new("--language")
        {
            Description = "The programming language for which to generate code.",
            DefaultValueFactory = _ => string.Empty
        };
        Option<bool> debugOption = new("--debug")
        {
            Description = "Enable debug mode.",
            DefaultValueFactory = _ => false
        };

        RootCommand rootCommand = new("LeetGen - A LeetCode code generator")
        {
            outputDirectoryOption,
            templateDirectoryOption,
            languageOption,
            debugOption,
            problemNumberOption
        };
        ParseResult parseResult = rootCommand.Parse(args);

        ApplicationOptions options = new();
        if (parseResult.Errors.Count == 0)
        {
            options.OutputDirectory = parseResult.GetValue<string>(outputDirectoryOption) ?? "./debug-output";
            options.TemplateDirectory = parseResult.GetValue<string>(templateDirectoryOption) ?? "./debug-templates";
            options.Language = parseResult.GetValue<string>(languageOption) ?? string.Empty;
            options.isDebug = parseResult.GetValue<bool>(debugOption);
            options.ProblemNumber = parseResult.GetValue<int>(problemNumberOption);
        }
        else
        {
            Console.Error.WriteLine("Error parsing command line arguments:");
            foreach (var error in parseResult.Errors)
            {
                Console.Error.WriteLine($"  {error.Message}");
            }   
        }
        return options;
    }
}
