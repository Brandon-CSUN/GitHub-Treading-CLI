using System.Text.Json;
using System.Text.Json.Serialization;

namespace GitHubTreadingCLI;

public class Program
{
    private static readonly HttpClient client = new HttpClient();
    public static async Task Main(string[] args)
    {
        client.DefaultRequestHeaders.Add("User-Agent", "GithubTreadingCLI-LearningApp");
        
        string url = "https://api.github.com/search/repositories?q=language:csharp&sort=stars&order=desc";

        Console.WriteLine("Fetching C# repositories from Github...");

        string rawJson = await client.GetStringAsync(url);

        GitHubSearchResponse? response = JsonSerializer.Deserialize<GitHubSearchResponse>(rawJson);

        if(response?.Items != null)
        {
            Console.WriteLine("\nFound top {response.Items.Count} C# repositories:\n");

            foreach(var repo in response.Items.Take(5))
            {
               Console.WriteLine($"⭐ {repo.Stars} | {repo.Name}");
               Console.WriteLine($" Description: {repo.Description}");
               Console.WriteLine($" URL: {repo.Url}");
            }
        }
    }

    public static async Task<string> FetchCSharpReposAsync()
    {
        string url = "https://api.github.com/search/repositories?q=language:csharp&sort=stars&order=desc";

        string jsonRespone = await client.GetStringAsync(url);

        return jsonRespone;
    }

    public record GitHubSearchResponse(
        [property: JsonPropertyName("items")] List<Repository> Items
    );

    public record Repository(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("html_url")] string Url,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("stargazers_count")] int Stars
    );
}
