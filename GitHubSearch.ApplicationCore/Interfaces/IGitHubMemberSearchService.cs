using GitHubSearch.ApplicationCore.Models;
using System.Threading.Tasks;

namespace GitHubSearch.ApplicationCore.Interfaces
{
    public interface IGitHubMemberSearchService
    {
        /// <summary>
        /// Gets a Github user object from the Github API
        /// </summary>
        /// <param name="username">The username of the Github user</param>
        /// <returns>An generic result object containing a Github user object</returns>
        Task<GenericResult<GitHubUser>> GetGitHubUserByUsername(string username);

        /// <summary>
        /// Gets a list of Github repo objects from the Github API
        /// </summary>
        /// <param name="username">The username of the Github user</param>
        /// <param name="countToTake">The max of repos to return from the list, if default 0 then returns them all</param>
        /// <returns>An generic result object containing a collection of Github repo objects</returns>
        Task<GenericResult<GitHubRepo>> GetGitHubUserReposByUsername(string username, uint countToTake = 0);
    }
}