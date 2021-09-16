using GitHubSearch.ApplicationCore.Interfaces;
using GitHubSearch.ApplicationCore.Models;
using RestSharp;
using System.Threading.Tasks;

namespace GitHubSearch.ApplicationCore.Services
{
    public class ApiHandler : IApiHandler
    {
        public async Task<IRestResponse> GetResponseFromApi(ApiConfig config)
        {
            RestClient client = new RestClient(config.EndPoint);

            return await client.ExecuteAsync(config.Request);
        }
    }
}