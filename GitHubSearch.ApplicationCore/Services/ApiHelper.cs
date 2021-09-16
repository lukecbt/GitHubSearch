using GitHubSearch.ApplicationCore.Enums;
using GitHubSearch.ApplicationCore.Interfaces;
using Newtonsoft.Json;
using System.Net;

namespace GitHubSearch.ApplicationCore.Services
{
    public class ApiHelper : IApiHelper
    {
        public T DeserializeObject<T>(string httpResponseContent)
        {
            return JsonConvert.DeserializeObject<T>(httpResponseContent);
        }

        public string ProcessErrorMessage(StatusCode statusCode)
        {
            switch (statusCode)
            {
                case StatusCode.BadRequest:
                    return "There was an error performing this request.";

                case StatusCode.NotFound:

                case StatusCode.NoContent:
                    return "The request yielded no results.";

                case StatusCode.InternalServerError:
                    return "The service is unavailable at this time.";

                default:
                    return "An error has occurred.";
            }
        }

        public StatusCode ProcessStatusCode(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.OK:
                    return StatusCode.OK;

                case HttpStatusCode.BadRequest:
                    return StatusCode.BadRequest;

                case HttpStatusCode.NotFound:
                    return StatusCode.NotFound;

                case HttpStatusCode.NoContent:
                    return StatusCode.NoContent;

                case HttpStatusCode.InternalServerError:
                    return StatusCode.InternalServerError;

                default:
                    return StatusCode.Error;
            }
        }
    }
}