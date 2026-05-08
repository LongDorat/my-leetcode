using System;

namespace LeetGen;

public interface ILanguageHandler
{
    public string LanguageSlug { get; }
    public bool ReplacePlaceHolders();
    public bool Initialize();
}
