using Newtonsoft.Json;
using System.Collections.Generic;

namespace GitHubSearch.ApplicationCore.Models
{
    public class GitHubUser
    {
        public string Login { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Bio { get; set; }

        public string Blog { get; set; }

        public int Followers { get; set; }

        public int Following { get; set; }

        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("repos_url")]
        public string ReposUrl { get; set; }

        public string FollowersHtmlUrl { get; set; }

        public string FollowingHtmlUrl { get; set; }

        public IEnumerable<GitHubRepo> Repos { get; set; }
    }
}