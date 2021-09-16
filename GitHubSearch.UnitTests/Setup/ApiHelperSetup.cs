using GitHubSearch.ApplicationCore.Interfaces;
using GitHubSearch.ApplicationCore.Services;

namespace GitHubSearch.UnitTests.Setup
{
    public class ApiHelperSetup
    {
        public readonly IApiHelper apiHelper;

        public ApiHelperSetup()
        {
            apiHelper = new ApiHelper();
        }
    }
}