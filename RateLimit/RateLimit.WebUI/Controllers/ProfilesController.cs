using Microsoft.AspNetCore.Mvc;
using RateLimit.BLL;
using RateLimit.BLL.Services.Configuration;
using RateLimit.BLL.Services.ProfileService;
using RateLimit.WebUI.Filters;
using RateLimit.WebUI.Models.Profile;
using System.Linq;
using RateLimit.WebUI.Utils;

namespace RateLimit.WebUI.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly IProfileService _profileService;

        private readonly string _path;

        public ProfilesController(IProfileService profileService, IConfig config)
        {
            _profileService = profileService;
            _path = config.GetJsonPath();
        }

        [RequestsCountRestrictor(count: 1)]
        public ActionResult Index(ProfilesConfigModel configModel)
        {
            var converted = Convert.To<BLL.Models.Profile.ProfilesConfigModel>(configModel);
            var viewModel = _profileService.Search(
                converted,
                _path
            );

            ViewData["SortState"] = configModel.SortState;
            ViewData["Filter"] = configModel.Filter;

            return View("Views/Profiles/Profiles.cshtml", Convert.To<ProfilesViewModel>(viewModel));
        }
    }
}