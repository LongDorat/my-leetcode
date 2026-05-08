namespace LeetGen.LanguageHandlers;

public interface ILanguageHandler
{
    public string LanguageSlug { get; }
    public bool ReplacePlaceHolders();
    public bool Initialize();
}
