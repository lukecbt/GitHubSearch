using GitHubSearch.ApplicationCore.Interfaces;
using GitHubSearch.ApplicationCore.Services;

namespace GitHubSearch.UnitTests.Setup
{
    public class ApiHandlerSetup
    {
        public readonly IApiHandler apiHandler;

        public ApiHandlerSetup()
        {
            apiHandler = new ApiHandler();
        }
    }
}