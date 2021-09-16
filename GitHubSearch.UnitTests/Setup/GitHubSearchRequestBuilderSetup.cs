using GitHubSearch.ApplicationCore.Interfaces;
using GitHubSearch.ApplicationCore.Services;

namespace GitHubSearch.UnitTests.Setup
{
    public class GitHubSearchRequestBuilderSetup
    {
        public readonly IGitHubSearchRequestBuilder requestBuilder;

        public GitHubSearchRequestBuilderSetup()
        {
            requestBuilder = new GitHubSearchRequestBuilder();
        }
    }
}