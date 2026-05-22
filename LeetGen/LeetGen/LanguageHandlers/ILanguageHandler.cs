namespace LeetGen.LanguageHandlers;

public interface ILanguageHandler
{
    public string LanguageSlug { get; }
    public bool ReplacePlaceHolders(GenerationPlan plan);
    public bool Initialize(GenerationPlan plan);
}
