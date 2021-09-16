using GitHubSearch.ApplicationCore.Enums;
using System.Net;

namespace GitHubSearch.ApplicationCore.Interfaces
{
    public interface IApiHelper
    {
        /// <summary>
        /// Deserialize Http response content to specified object type using Newtonsoft
        /// </summary>
        /// <typeparam name="T">Type of model to parse into</typeparam>
        /// <param name="httpResponseContent">The response content with the JSON to parse</param>
        /// <returns>An object of the specified type</returns>
        T DeserializeObject<T>(string httpResponseContent);

        /// <summary>
        /// Converts HttpStatusCode into this projects status code enum
        /// </summary>
        /// <param name="statusCode">HttpStatusCode to convert</param>
        /// <returns>A StatusCode object reflecting the HttpStatusCode</returns>
        StatusCode ProcessStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Returns an error message related to the StatusCode provided
        /// </summary>
        /// <param name="statusCode">StatusCode to return a message for</param>
        /// <returns>A string detailing the status code</returns>
        string ProcessErrorMessage(StatusCode statusCode);
    }
}