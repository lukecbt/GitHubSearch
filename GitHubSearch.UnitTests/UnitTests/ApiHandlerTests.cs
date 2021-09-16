using FluentAssertions;
using FluentAssertions.Execution;
using GitHubSearch.ApplicationCore.Models;
using GitHubSearch.UnitTests.Setup;
using RestSharp;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace GitHubSearch.UnitTests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class ApiHandlerTests : IClassFixture<ApiHandlerSetup>
    {
        private readonly ApiHandlerSetup _fixture;
        private const string _getUserByUsernameUrl = "https://api.github.com/users/lukecbt";

        public ApiHandlerTests(ApiHandlerSetup fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CreateApiHandler()
        {
            // Arrange

            // Act

            // Assert
            _fixture.apiHandler.Should().NotBeNull();
        }

        [Fact]
        public void GetResponseFromApi_VerifyClientPerformsSuccessfulGetRequest()
        {
            // Arrange
            IRestRequest request = new RestRequest(Method.GET);

            request.AddHeader("User-Agent", "Request");

            ApiConfig config = new ApiConfig
            {
                EndPoint = _getUserByUsernameUrl,
                Request = request
            };

            // Act
            var result = Task.Run(async () => await _fixture.apiHandler.GetResponseFromApi(config)).GetAwaiter().GetResult();

            // Assert
            using (new AssertionScope())
            {
                result.StatusCode.Should().Be(HttpStatusCode.OK);
                result.Content.Should().NotBeNull();
            }
        }
    }
}