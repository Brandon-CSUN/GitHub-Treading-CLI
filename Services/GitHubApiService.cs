using System.Text.Json;
using System.Text.Json.Nodes;

namespace GitHubTreadingCLI;

public class GitHubApiService
{
    private static readonly HttpClient _httpClient = new HttpClient();

    static GitHubApiService()
    {
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubTreadingCLI-APP");
    }

    public async Task<List<Repository>> GetTreadingRepositoriesAsync(string language, string since)
    {
        
        string dateFilter = GetCreatedDateFilter(since);
        string url = $"https://api.github.com/search/repositories?q=language:{language}+created:>{dateFilter}&sort=stars&order=desc";

        string rawJson = await _httpClient.GetStringAsync(url);
        var response = JsonSerializer.Deserialize<GitHubSearchRespone>(rawJson);

        return response?.Items ?? new List<Repository>();
    }

    private string GetCreatedDateFilter(string since)
    {
        DateTime now = DateTime.UtcNow;
        DateTime filterDate = since.ToLower() switch
        {
            "weekly" => now.AddDays(-7),
            "monthly" => now.AddDays(-30),
            _ => now.AddDays(-1)
        };
        
        return filterDate.ToString("yyyy-MM-dd");
    }
}
