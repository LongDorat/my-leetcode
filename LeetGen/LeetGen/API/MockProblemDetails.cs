using System;

namespace LeetGen.API;

public class MockProblemDetails : IProblemDetailsAPI
{
    public string Title { get; private set; }
    public string Slug { get; private set; }

    public MockProblemDetails()
    {
        Title = "Two Sum";
        Slug = "two-sum";
    }

    public void FetchDetails()
    {
        // Mock implementation, does nothing
    }
}
