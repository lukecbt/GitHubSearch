using RestSharp;

namespace GitHubSearch.ApplicationCore.Models
{
    public class ApiConfig
    {
        public string EndPoint { get; set; }
        public IRestRequest Request { get; set; }
    }
}