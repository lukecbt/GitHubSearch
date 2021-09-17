using Newtonsoft.Json;

namespace GitHubSearch.ApplicationCore.Models
{
    public class GitHubRepo
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Language { get; set; }

        [JsonProperty("stargazers_count")]
        public int StargazersCount { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
    }
}