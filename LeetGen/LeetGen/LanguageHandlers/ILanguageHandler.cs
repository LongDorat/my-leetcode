namespace LeetGen.LanguageHandlers;

public interface ILanguageHandler
{
    public string LanguageSlug { get; }
    public bool ReplacePlaceHolders(string outputPath);
    public bool Initialize(string outputPath);
}
