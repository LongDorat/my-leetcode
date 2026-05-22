using System.Text.Json;

namespace LeetGen.API;

public class LeetCodeDetails : IProblemDetailsAPI
{
    private static readonly HttpClient HttpClient = new();
    private const string AllProblemsEndpoint = "https://leetcode.com/api/problems/all/";

    public string Title { get; private set; } = string.Empty;
    public string Slug { get; private set; } = string.Empty;
    public void FetchDetails(int problemId)
    {
        if (problemId <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(problemId), "Problem ID must be a positive integer.");
        }

        using var request = new HttpRequestMessage(HttpMethod.Get, AllProblemsEndpoint);
        if (!HttpClient.DefaultRequestHeaders.Contains("User-Agent"))
        {
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "LeetGen/1.0");
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
        }

        using var response = HttpClient.Send(request);
        string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

        if (!response.IsSuccessStatusCode)
        {
            CustomConsole.WriteLine($"LeetCode API error: {(int)response.StatusCode} {response.ReasonPhrase}", new MessageType("error"));
            throw new InvalidOperationException("Failed to fetch problem details from LeetCode API.");
        }

        using JsonDocument document = JsonDocument.Parse(responseBody);
        var root = document.RootElement;
        if (!root.TryGetProperty("stat_status_pairs", out var pairs) || pairs.ValueKind != JsonValueKind.Array)
        {
            CustomConsole.WriteLine("LeetCode API response missing stat_status_pairs.", new MessageType("error"));
            throw new InvalidOperationException("LeetCode API response missing problem list.");
        }

        string? title = null;
        string? slug = null;
        foreach (var pair in pairs.EnumerateArray())
        {
            if (!pair.TryGetProperty("stat", out var stat))
            {
                continue;
            }

            if (!stat.TryGetProperty("frontend_question_id", out var idElement))
            {
                continue;
            }

            int idValue;
            if (idElement.ValueKind == JsonValueKind.Number)
            {
                if (!idElement.TryGetInt32(out idValue))
                {
                    continue;
                }
            }
            else if (idElement.ValueKind == JsonValueKind.String)
            {
                if (!int.TryParse(idElement.GetString(), out idValue))
                {
                    continue;
                }
            }
            else
            {
                continue;
            }

            if (idValue != problemId)
            {
                continue;
            }

            title = stat.TryGetProperty("question__title", out var titleElement)
                ? titleElement.GetString()
                : null;
            slug = stat.TryGetProperty("question__title_slug", out var slugElement)
                ? slugElement.GetString()
                : null;
            break;
        }

        Title = title ?? string.Empty;
        Slug = slug ?? string.Empty;

        if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Slug))
        {
            CustomConsole.WriteLine("LeetCode API response did not include title or slug.", new MessageType("error"));
            throw new InvalidOperationException("LeetCode API response did not include title or slug.");
        }
    }
}