using Microsoft.AspNetCore.Mvc;
using RateLimit.BLL.Services.ProfileService;
using RateLimit.WebUI.Filters;
using RateLimit.WebUI.Models.Profile;
using RateLimit.WebUI.Utils;

namespace RateLimit.WebUI.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfilesController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [RequestsCountRestrictor(count: 1)]
        public ActionResult Index(ProfilesConfigModel configModel)
        {
            var viewModel = _profileService.Search(
                Convert.To<ProfilesConfigModel, BLL.Models.Profile.ProfilesConfigModel>(configModel)
            );

            ViewData["SortState"] = configModel.SortState;
            ViewData["Filter"] = configModel.Filter;

            return View("Views/Profiles/Profiles.cshtml", Convert.To<BLL.Models.Profile.SearchResult, ProfilesViewModel>(viewModel));
        }
    }
}