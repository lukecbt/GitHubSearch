using GitHubSearch.ApplicationCore.Enums;
using GitHubSearch.ApplicationCore.Interfaces;
using GitHubSearch.ApplicationCore.Models;
using Moq;
using RestSharp;
using System.Net;

namespace GitHubSearch.UnitTests.Mocks
{
    public static class GitHubMemberSearchServiceMocks
    {
        #region Define Mocks

        public static void MockBuildGetGitHubUserUrl(Mock<IGitHubSearchRequestBuilder> mock, string stringToReturn)
        {
            mock.Setup(m => m.BuildGetGitHubUserUrl(It.IsAny<string>())).Returns(stringToReturn);
        }

        public static void MockBuildGetGitHubRepoUrl(Mock<IGitHubSearchRequestBuilder> mock, string stringToReturn)
        {
            mock.Setup(m => m.BuildGetGitHubReposUrl(It.IsAny<string>())).Returns(stringToReturn);
        }

        public static void MockBuildGitHubSearchRequest(Mock<IGitHubSearchRequestBuilder> mock, IRestRequest requestToReturn)
        {
            mock.Setup(m => m.BuildGitHubSearchRequest()).Returns(requestToReturn);
        }

        public static void MockGetResponseFromApi(Mock<IApiHandler> mock, IRestResponse responseToReturn)
        {
            mock.Setup(m => m.GetResponseFromApi(It.IsAny<ApiConfig>())).ReturnsAsync(responseToReturn);
        }

        public static void MockProcessStatusCode(Mock<IApiHelper> mock, StatusCode statusCodeToReturn)
        {
            mock.Setup(m => m.ProcessStatusCode(It.IsAny<HttpStatusCode>())).Returns(statusCodeToReturn);
        }

        public static void MockProcessErrorMessage(Mock<IApiHelper> mock, string stringToReturn)
        {
            mock.Setup(m => m.ProcessErrorMessage(It.IsAny<StatusCode>())).Returns(stringToReturn);
        }

        public static void MockDeserializeObject<T>(Mock<IApiHelper> mock, T objToReturn)
        {
            mock.Setup(m => m.DeserializeObject<T>(It.IsAny<string>())).Returns(objToReturn);
        }

        #endregion Define Mocks
    }
}