using System;

namespace LeetGen;

public class ApplicationOptions
{
    public string OutputDirectory { get; set; } = string.Empty;
    public string TemplateDirectory { get; set; } = string.Empty;
    public int ProblemNumber { get; set; } = 0;
    public string Language { get; set; } = string.Empty;
    public bool isDebug { get; set; } = false;
}
