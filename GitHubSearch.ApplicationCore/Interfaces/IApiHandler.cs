using GitHubSearch.ApplicationCore.Models;
using RestSharp;
using System.Threading.Tasks;

namespace GitHubSearch.ApplicationCore.Interfaces
{
    public interface IApiHandler
    {
        /// <summary>
        /// Perform a get request to the specified api url
        /// </summary>
        /// <param name="config">The api config setting used to perform the request</param>
        /// <returns>An IRestResponse object containing the response</returns>
        Task<IRestResponse> GetResponseFromApi(ApiConfig config);
    }
}