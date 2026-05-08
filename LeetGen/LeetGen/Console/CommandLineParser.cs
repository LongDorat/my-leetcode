using System.CommandLine;

namespace LeetGen.Console;

public class CommandLineParser
{
    private readonly Option<string> _outputDirectoryOption = new("--output")
    {
        Description = "The directory where generated files will be saved.",
        DefaultValueFactory = _ => "./debug-output"
    };
    private readonly Option<string> _templateDirectoryOption = new("--template")
    {
        Description = "The directory where template files are located.",
        DefaultValueFactory = _ => "./debug-templates"
    };
    private readonly Option<int> _problemNumberOption = new("--problem")
    {
        Description = "The LeetCode problem number to generate code for.",
        DefaultValueFactory = _ => 0
    };
    private readonly Option<string> _languageOption = new("--language")
    {
        Description = "The programming language for which to generate code.",
        DefaultValueFactory = _ => string.Empty
    };
    private readonly Option<bool> _debugOption = new("--debug")
    {
        Description = "Enable debug mode.",
        DefaultValueFactory = _ => false
    };

    public ApplicationOptions Parse(string[] args)
    {
        RootCommand rootCommand = new("LeetGen - A LeetCode problem template generator");
        Command createCommand = BuildCreateCommand();
        rootCommand.Add(createCommand);
        ParseResult parseResult = rootCommand.Parse(args);

        ApplicationOptions options = new();
        if (parseResult.Errors.Count == 0)
        {
            options.OutputDirectory = parseResult.GetValue<string>(_outputDirectoryOption) ?? "./debug-output";
            options.TemplateDirectory = parseResult.GetValue<string>(_templateDirectoryOption) ?? "./debug-templates";
            options.Language = parseResult.GetValue<string>(_languageOption) ?? string.Empty;
            options.isDebug = parseResult.GetValue<bool>(_debugOption);
            options.ProblemNumber = parseResult.GetValue<int>(_problemNumberOption);
        }
        else
        {
            CustomConsole.WriteLine("Error parsing command line arguments!", new MessageType("error"));
            CustomConsole.WriteLine("Please use the provided scripts to run the application!", new MessageType("error"));
            Environment.Exit(1);
        }
        return options;
    }

    private Command BuildCreateCommand()
    {
        Command createCommand = new("create", "Generate a LeetCode problem template")
        {
            _outputDirectoryOption,
            _templateDirectoryOption,
            _languageOption,
            _debugOption,
            _problemNumberOption
        };

        return createCommand;
    }
}
