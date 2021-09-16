using GitHubSearch.ApplicationCore.Interfaces;
using RestSharp;

namespace GitHubSearch.ApplicationCore.Services
{
    public class GitHubSearchRequestBuilder : IGitHubSearchRequestBuilder
    {
        private const string _getUserByUsernameUrl = "https://api.github.com/users/{username}";
        private const string _getReposByUsernameUrl = "https://api.github.com/users/{username}/repos";

        public string BuildGetGitHubUserUrl(string username)
        {
            return _getUserByUsernameUrl.Replace("{username}", username);
        }

        public string BuildGetGitHubReposUrl(string username)
        {
            return _getReposByUsernameUrl.Replace("{username}", username);
        }

        public IRestRequest BuildGitHubSearchRequest()
        {
            IRestRequest request = new RestRequest(Method.GET);

            request.AddHeader("User-Agent", "GitHub-User-Agent");

            return request;
        }
    }
}