using System.Text.Json.Serialization;

namespace GitHubTreadingCLI;
    public record GitHubSearchRespone(
        [property: JsonPropertyName("items")] List<Repository> Items
    );
    public record Repository(
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("owner")] RepositoryOwner Owner,
        [property: JsonPropertyName("html_url")] string Html_url,
        [property: JsonPropertyName("description")] string Description,
        [property: JsonPropertyName("stargazers_count")] int Stars,
        [property: JsonPropertyName("language")] string language
    );
    public record RepositoryOwner(
        [property: JsonPropertyName("login")] string Login
    );

