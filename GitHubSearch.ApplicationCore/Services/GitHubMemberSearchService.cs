using GitHubSearch.ApplicationCore.Enums;
using GitHubSearch.ApplicationCore.Interfaces;
using GitHubSearch.ApplicationCore.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubSearch.ApplicationCore.Services
{
    public class GitHubMemberSearchService : IGitHubMemberSearchService
    {
        private const string _gitHubUrl = "https://github.com";

        private readonly IApiHandler _apiHandler;
        private readonly IGitHubSearchRequestBuilder _requestBuilder;
        private readonly IApiHelper _apiHelper;

        public GitHubMemberSearchService()
        {
        }

        public GitHubMemberSearchService(IApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }

        public GitHubMemberSearchService(IApiHandler apiHandler, IGitHubSearchRequestBuilder requestBuilder, IApiHelper apiHelper)
        {
            _apiHandler = apiHandler;
            _requestBuilder = requestBuilder;
            _apiHelper = apiHelper;
        }

        public async Task<GenericResult<GitHubUser>> GetGitHubUserByUsername(string username)
        {
            var result = new GenericResult<GitHubUser>();
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new ArgumentException("Username cannot be empty", nameof(username));
                }

                // create the url and request settings
                ApiConfig config = new ApiConfig
                {
                    EndPoint = _requestBuilder.BuildGetGitHubUserUrl(username),
                    Request = _requestBuilder.BuildGitHubSearchRequest()
                };

                // log calling $"Calling:- '{config.EndPoint}'. Request:- {config.Request.ToJson()}"
                IRestResponse response = await _apiHandler.GetResponseFromApi(config).ConfigureAwait(false);
                // log status $"Call:- '{config.EndPoint}'. Response:- {response.StatusCode}"

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    // to do log fail $"Response to '{config.EndPoint}' was unsuccessful. StatusCode:- '{response.StatusCode}'. Content:- '{response.Content}'"
                    result.Status = _apiHelper.ProcessStatusCode(response.StatusCode);
                    result.Message = _apiHelper.ProcessErrorMessage(result.Status);
                    return result;
                }

                // parse response to model
                GitHubUser user = _apiHelper.DeserializeObject<GitHubUser>(response.Content);

                // get follower/following url https://github.com/lukecbt?tab=following
                user.FollowersHtmlUrl = GetFollowersHtmlUrl(username);
                user.FollowingHtmlUrl = GetFollowingHtmlUrl(username);

                // log success
                result.Status = StatusCode.OK;
                result.Item = user;
                return result;
            }
            catch (Exception ex)
            {
                // log ex
                throw;
            }
        }

        public async Task<GenericResult<GitHubRepo>> GetGitHubUserReposByUsername(string username, uint countToTake = 0)
        {
            var result = new GenericResult<GitHubRepo>();
            try
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    throw new ArgumentException("Username cannot be empty", nameof(username));
                }

                // create the url and request settings
                ApiConfig config = new ApiConfig
                {
                    EndPoint = _requestBuilder.BuildGetGitHubReposUrl(username),
                    Request = _requestBuilder.BuildGitHubSearchRequest()
                };

                // to do log calling $"Calling:- '{config.EndPoint}'. Request:- {config.Request.ToJson()}"
                IRestResponse response = await _apiHandler.GetResponseFromApi(config);
                // to do log status $"Call:- '{config.EndPoint}'. Response:- {response.StatusCode}"

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    // to do log fail $"Response to '{config.EndPoint}' was unsuccessful. StatusCode:- '{response.StatusCode}'. Content:- '{response.Content}'"
                    result.Status = _apiHelper.ProcessStatusCode(response.StatusCode);
                    result.Message = _apiHelper.ProcessErrorMessage(result.Status);
                    return result;
                }

                // parse response to model
                IEnumerable<GitHubRepo> repos = _apiHelper.DeserializeObject<IEnumerable<GitHubRepo>>(response.Content).OrderByDescending(x => x.StargazersCount);

                if (!countToTake.Equals(0))
                {
                    repos = repos.Take((int)countToTake);
                }

                // to do log success
                result.Status = StatusCode.OK;
                result.Items = repos;
                return result;
            }
            catch (Exception ex)
            {
                // to do log ex
                throw;
            }
        }

        #region Helpers
        private string GetFollowersHtmlUrl(string username)
        {
            return string.Format("{0}/{1}?tab=followers", _gitHubUrl, username);
        }

        private string GetFollowingHtmlUrl(string username)
        {
            return string.Format("{0}/{1}?tab=following", _gitHubUrl, username);
        }
        #endregion
    }
}