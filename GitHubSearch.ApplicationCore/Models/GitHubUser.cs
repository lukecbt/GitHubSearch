using Newtonsoft.Json;
using System.Collections.Generic;

namespace GitHubSearch.ApplicationCore.Models
{
    public class GitHubUser
    {
        public string Login { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        [JsonProperty("repos_url")]
        public string ReposUrl { get; set; }

        public ICollection<GitHubRepo> Repos { get; set; }
    }
}