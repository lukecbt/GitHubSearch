using FluentAssertions;
using FluentAssertions.Execution;
using GitHubSearch.UnitTests.Setup;
using RestSharp;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace GitHubSearch.UnitTests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class GitHubSearchRequestBuilderTests : IClassFixture<GitHubSearchRequestBuilderSetup>
    {
        private readonly GitHubSearchRequestBuilderSetup _fixture;

        private const string username = "lukecbt";

        public GitHubSearchRequestBuilderTests(GitHubSearchRequestBuilderSetup fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void BuildGetGithubUserUrl_ReturnsCorrectUsernameUrl()
        {
            // Arrange
            const string correctUrl = "https://api.github.com/users/lukecbt";

            // Act
            var result = _fixture.requestBuilder.BuildGetGitHubUserUrl(username);

            // Assert
            result.Should().Be(correctUrl);
        }

        [Fact]
        public void BuildGetGithubRepoUrl_ReturnsCorrectUsernameUrl()
        {
            // Arrange
            const string correctUrl = "https://api.github.com/users/lukecbt/repos";

            // Act
            var result = _fixture.requestBuilder.BuildGetGitHubReposUrl(username);

            // Assert
            result.Should().Be(correctUrl);
        }

        [Fact]
        public void BuildGithubSearchRequest_ReturnsCorrectGetRequestForGithubUserSearch()
        {
            // Arrange

            // Act
            var result = _fixture.requestBuilder.BuildGitHubSearchRequest();

            // Assert
            using (new AssertionScope())
            {
                result.Method.Should().Be(Method.GET);
                result.Parameters.Should().HaveCount(1).And.ContainSingle();
                result.Parameters.FirstOrDefault().Name.Should().Be("User-Agent");
                result.Parameters.FirstOrDefault().Value.Should().Be("GitHub-User-Agent");
            }
        }
    }
}