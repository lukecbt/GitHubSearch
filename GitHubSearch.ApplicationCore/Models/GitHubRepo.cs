using Newtonsoft.Json;

namespace GitHubSearch.ApplicationCore.Models
{
    public class GitHubRepo
    {
        public string Name { get; set; }

        [JsonProperty("full_Name")]
        public string FullName { get; set; }

        public string Description { get; set; }

        [JsonProperty("stargazers_count")]
        public int StargazersCount { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
    }
}