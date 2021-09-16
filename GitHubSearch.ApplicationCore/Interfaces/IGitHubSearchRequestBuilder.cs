using RestSharp;

namespace GitHubSearch.ApplicationCore.Interfaces
{
    public interface IGitHubSearchRequestBuilder
    {
        /// <summary>
        /// Build the request for executing a search on the Github api
        /// </summary>
        /// <returns>An IRestRequest object containing the request</returns>
        IRestRequest BuildGitHubSearchRequest();

        /// <summary>
        /// Build the get Github user by username url
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Github api url</returns>
        string BuildGetGitHubUserUrl(string username);

        /// <summary>
        /// Build the get Github repos by username url
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Github api url</returns>
        string BuildGetGitHubReposUrl(string username);
    }
}