using FluentAssertions;
using GitHubSearch.ApplicationCore.Enums;
using GitHubSearch.ApplicationCore.Models;
using GitHubSearch.UnitTests.Setup;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text;
using Xunit;

namespace GitHubSearch.UnitTests.UnitTests
{
    [ExcludeFromCodeCoverage]
    public class ApiHelperTests : IClassFixture<ApiHelperSetup>
    {
        private readonly ApiHelperSetup _fixture;

        public ApiHelperTests(ApiHelperSetup fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CreateApiHelper()
        {
            // Arrange

            // Act

            // Assert
            _fixture.apiHelper.Should().NotBeNull();
        }

        #region ProcessStatusCode Tests

        [Fact]
        public void ProcessStatusCode_HttpStatusCodeOKReturnsSuccess()
        {
            // Arrange
            const HttpStatusCode httpStatusCode = HttpStatusCode.OK;

            // Act
            StatusCode code = _fixture.apiHelper.ProcessStatusCode(httpStatusCode);

            // Assert
            code.Should().Be(StatusCode.OK);
        }

        [Fact]
        public void ProcessStatusCode_HttpStatusCodeBadRequestReturnsBadRequest()
        {
            // Arrange
            const HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest;

            // Act
            StatusCode code = _fixture.apiHelper.ProcessStatusCode(httpStatusCode);

            // Assert
            code.Should().Be(StatusCode.BadRequest);
        }

        [Fact]
        public void ProcessStatusCode_HttpStatusCodeNotFoundReturnsNotFound()
        {
            // Arrange
            const HttpStatusCode httpStatusCode = HttpStatusCode.NotFound;

            // Act
            StatusCode code = _fixture.apiHelper.ProcessStatusCode(httpStatusCode);

            // Assert
            code.Should().Be(StatusCode.NotFound);
        }

        [Fact]
        public void ProcessStatusCode_HttpStatusCodeNoContentReturnsNoContent()
        {
            // Arrange
            const HttpStatusCode httpStatusCode = HttpStatusCode.NoContent;

            // Act
            StatusCode code = _fixture.apiHelper.ProcessStatusCode(httpStatusCode);

            // Assert
            code.Should().Be(StatusCode.NoContent);
        }

        [Fact]
        public void ProcessStatusCode_HttpStatusCodeInternalServerErrorReturnsInternalServerError()
        {
            // Arrange
            const HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            // Act
            StatusCode code = _fixture.apiHelper.ProcessStatusCode(httpStatusCode);

            // Assert
            code.Should().Be(StatusCode.InternalServerError);
        }

        [Theory]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.BadGateway)]
        [InlineData(HttpStatusCode.GatewayTimeout)]
        public void ProcessStatusCode_ReturnsError(HttpStatusCode httpStatusCode)
        {
            // Arrange

            // Act
            StatusCode code = _fixture.apiHelper.ProcessStatusCode(httpStatusCode);

            // Assert
            code.Should().Be(StatusCode.Error);
        }

        #endregion ProcessStatusCode Tests

        #region ProcessErrorMessage Tests

        [Fact]
        public void ProcessErrorMessage_ReturnsBadRequestErrorMessage()
        {
            // Arrange

            // Act
            string message = _fixture.apiHelper.ProcessErrorMessage(StatusCode.BadRequest);

            // Assert
            message.Should().Be("There was an error performing this request.");
        }

        [Fact]
        public void ProcessErrorMessage_ReturnsNotFoundErrorMessage()
        {
            // Arrange

            // Act
            string message = _fixture.apiHelper.ProcessErrorMessage(StatusCode.NotFound);

            // Assert
            message.Should().Be("The request yielded no results.");
        }

        [Fact]
        public void ProcessErrorMessage_ReturnsNoContentErrorMessage()
        {
            // Arrange

            // Act
            string message = _fixture.apiHelper.ProcessErrorMessage(StatusCode.NoContent);

            // Assert
            message.Should().Be("The request yielded no results.");
        }

        [Fact]
        public void ProcessErrorMessage_ReturnsInternalServerErrorMessage()
        {
            // Arrange

            // Act
            string message = _fixture.apiHelper.ProcessErrorMessage(StatusCode.InternalServerError);

            // Assert
            message.Should().Be("The service is unavailable at this time.");
        }

        [Fact]
        public void ProcessErrorMessage_ReturnsGenericErrorMessage()
        {
            // Arrange

            // Act
            string message = _fixture.apiHelper.ProcessErrorMessage(StatusCode.Error);

            // Assert
            message.Should().Be("An error has occurred.");
        }

        #endregion ProcessErrorMessage Tests

        #region Deserializer Test

        [Fact]
        public void DeserializeObject_VerifyReturnObject()
        {
            // Arrange
            const string path = "../../Data/sample-github-response.json";
            string content = File.ReadAllText(path, Encoding.UTF8);

            // Act
            var result = _fixture.apiHelper.DeserializeObject<GitHubUser>(content);

            // Assert
            result.Login.Should().Be("lukecbt");
        }

        #endregion Deserializer Test
    }
}