using GitHubSearch.ApplicationCore.Enums;
using GitHubSearch.ApplicationCore.Interfaces;
using GitHubSearch.ApplicationCore.Models;
using GitHubSearch.ApplicationCore.Services;
using GitHubSearch.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GitHubSearch.Controllers
{
    public class HomeController : Controller
    {
        private IGitHubMemberSearchService _githubMemberSearchService;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RenderForm()
        {
            var vm = new GetUserFormModel();
            return PartialView("GetUserFormPartial", vm);
        }

        public async Task<ActionResult> GetUser(GetUserFormModel model)
        {
            GenericResult<GitHubUser> result = new GenericResult<GitHubUser>();

            if (ModelState.IsValid)
            {
                // mark form as submitted
                TempData["Submitted"] = true;

                _githubMemberSearchService = new GitHubMemberSearchService(new ApiHandler(), new GitHubSearchRequestBuilder(), new ApiHelper());

                try
                {
                    result = await _githubMemberSearchService.GetGitHubUserByUsername(model.Username);
                    var repoResult = await _githubMemberSearchService.GetGitHubUserReposByUsername(model.Username);
                    result.Item.Repos = repoResult.Items;
                }
                catch
                {
                    result.Status = StatusCode.Error;
                    result.Message = "An error has occurred.";
                }

                return View("Index", result);
            }

            return View("Index");
        }
    }
}