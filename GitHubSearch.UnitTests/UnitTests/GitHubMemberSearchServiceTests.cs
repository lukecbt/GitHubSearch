using FluentAssertions;
using FluentAssertions.Execution;
using GitHubSearch.ApplicationCore.Interfaces;
using GitHubSearch.ApplicationCore.Models;
using GitHubSearch.ApplicationCore.Services;
using GitHubSearch.UnitTests.Mocks;
using Moq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GitHubSearch.UnitTests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class GitHubMemberSearchServiceTests
    {
        private readonly IGitHubMemberSearchService _service;
        private readonly Mock<GitHubMemberSearchService> _mockService;
        private readonly Mock<IApiHandler> _mockApiHandler;
        private readonly Mock<IGitHubSearchRequestBuilder> _mockRequestBuilder;
        private readonly Mock<IApiHelper> _mockApiHelper;

        public GitHubMemberSearchServiceTests()
        {
            _service = new GitHubMemberSearchService(new ApiHandler(), new GitHubSearchRequestBuilder(), new ApiHelper());
            _mockApiHandler = new Mock<IApiHandler>();
            _mockRequestBuilder = new Mock<IGitHubSearchRequestBuilder>();
            _mockApiHelper = new Mock<IApiHelper>();
            _mockService = new Mock<GitHubMemberSearchService>(_mockApiHandler.Object, _mockRequestBuilder.Object, _mockApiHelper.Object);
        }

        [Fact]
        public void CreateGitHubMemberService()
        {
            // Arrange

            // Act

            // Assert
            _service.Should().NotBeNull();
        }

        #region GetGitHubUserByUsername Tests

        [Fact]
        public void GetGitHubUserByUsername_EmptyUsernameParamThrowsArgumentException()
        {
            // Arrange

            // Act
            Func<Task> act = async () => await _mockService.Object.GetGitHubUserByUsername(null);

            // Assert
            using (new AssertionScope())
            {
                act.Should().ThrowAsync<ArgumentException>().Result.And.Message.Should().StartWith("Username cannot be empty");
            }
        }

        [Fact]
        public void GetGitHubUserByUsername_UnsuccessfulResponseReturnsErrorResult()
        {
            // Assert
            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            GitHubMemberSearchServiceMocks.MockBuildGetGitHubUserUrl(_mockRequestBuilder, "GitHub_url");
            GitHubMemberSearchServiceMocks.MockBuildGitHubSearchRequest(_mockRequestBuilder, new RestRequest());
            GitHubMemberSearchServiceMocks.MockGetResponseFromApi(_mockApiHandler, response);

            // Act
            var result = Task.Run(async () => await _mockService.Object.GetGitHubUserByUsername("username")).GetAwaiter().GetResult();

            // Assert
            using (new AssertionScope())
            {
                _mockApiHelper.Verify(x => x.ProcessStatusCode(response.StatusCode), Times.Once);
                _mockApiHelper.Verify(x => x.ProcessErrorMessage(result.Status), Times.Once);
            }
        }

        [Fact]
        public void GetGitHubUserByUsername_SuccessResponseReturnsObject()
        {
            // Assert
            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "response_content"
            };

            var user = new GitHubUser
            {
                Name = "Luke"
            };

            GitHubMemberSearchServiceMocks.MockBuildGetGitHubUserUrl(_mockRequestBuilder, "GitHub_url");
            GitHubMemberSearchServiceMocks.MockBuildGitHubSearchRequest(_mockRequestBuilder, new RestRequest());
            GitHubMemberSearchServiceMocks.MockGetResponseFromApi(_mockApiHandler, response);
            GitHubMemberSearchServiceMocks.MockDeserializeObject(_mockApiHelper, user);

            // Act
            var result = Task.Run(async () => await _mockService.Object.GetGitHubUserByUsername("username")).GetAwaiter().GetResult();

            // Assert
            using (new AssertionScope())
            {
                _mockApiHelper.Verify(x => x.DeserializeObject<GitHubUser>(response.Content), Times.Once);
                result.Item.Should().Be(user, "a successful request to the GitHub api should return a response which gets parsed into a GitHub user object");
                result.Status.Should().Be(ApplicationCore.Enums.StatusCode.OK);
            }
        }

        #endregion GetGitHubUserByUsername Tests

        #region GetGitHubUserReposByUsername

        [Fact]
        public void GetGitHubUserReposByUsername_EmptyUsernameParamThrowsArgumentException()
        {
            // Arrange

            // Act
            Func<Task> act = async () => await _mockService.Object.GetGitHubUserReposByUsername(null);

            // Assert
            using (new AssertionScope())
            {
                act.Should().ThrowAsync<ArgumentException>().Result.And.Message.Should().StartWith("Username cannot be empty");
            }
        }

        [Fact]
        public void GetGitHubUserReposByUsername_UnsuccessfulResponseReturnsErrorResult()
        {
            // Assert
            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            GitHubMemberSearchServiceMocks.MockBuildGetGitHubRepoUrl(_mockRequestBuilder, "GitHub_url");
            GitHubMemberSearchServiceMocks.MockBuildGitHubSearchRequest(_mockRequestBuilder, new RestRequest());
            GitHubMemberSearchServiceMocks.MockGetResponseFromApi(_mockApiHandler, response);

            // Act
            var result = Task.Run(async () => await _mockService.Object.GetGitHubUserReposByUsername("username")).GetAwaiter().GetResult();

            // Assert
            using (new AssertionScope())
            {
                _mockApiHelper.Verify(x => x.ProcessStatusCode(response.StatusCode), Times.Once);
                _mockApiHelper.Verify(x => x.ProcessErrorMessage(result.Status), Times.Once);
            }
        }

        [Fact]
        public void GetGitHubUserReposByUsername_SuccessResponseReturnsObject()
        {
            // Assert
            var response = new RestResponse
            {
                StatusCode = HttpStatusCode.OK,
                Content = "response_content"
            };

            var repos = new List<GitHubRepo>
            {
                new GitHubRepo(),
                new GitHubRepo()
            };

            GitHubMemberSearchServiceMocks.MockBuildGetGitHubRepoUrl(_mockRequestBuilder, "GitHub_url");
            GitHubMemberSearchServiceMocks.MockBuildGitHubSearchRequest(_mockRequestBuilder, new RestRequest());
            GitHubMemberSearchServiceMocks.MockGetResponseFromApi(_mockApiHandler, response);
            GitHubMemberSearchServiceMocks.MockDeserializeObject<IEnumerable<GitHubRepo>>(_mockApiHelper, repos);

            // Act
            var result = Task.Run(async () => await _mockService.Object.GetGitHubUserReposByUsername("username")).GetAwaiter().GetResult();

            // Assert
            using (new AssertionScope())
            {
                _mockApiHelper.Verify(x => x.DeserializeObject<IEnumerable<GitHubRepo>>(response.Content), Times.Once);
                result.Items.Should().HaveCount(2, "a successful request to the GitHub api should return a response which gets parsed into a GitHub repo collection");
                result.Status.Should().Be(ApplicationCore.Enums.StatusCode.OK);
            }
        }

        #endregion GetGitHubUserReposByUsername
    }
}