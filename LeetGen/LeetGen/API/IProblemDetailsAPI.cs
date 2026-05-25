using System;

namespace LeetGen.API;

public interface IProblemDetailsAPI
{
    public string Title { get; }
    public string Slug { get; }
    public void FetchDetails(int problemId);
}
