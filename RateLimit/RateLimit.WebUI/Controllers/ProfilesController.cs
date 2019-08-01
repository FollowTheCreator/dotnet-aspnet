using Microsoft.AspNetCore.Mvc;
using RateLimit.BLL;
using RateLimit.BLL.Services.Configuration;
using RateLimit.BLL.Services.ProfileService;
using RateLimit.WebUI.Filters;
using RateLimit.WebUI.Models.Profile;
using System.Linq;

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

        private TOut To<TIn, TOut>(TIn input) where TOut : class
        {
            if (input.GetType() == typeof(ProfilesConfigModel))
            {
                var data = input as ProfilesConfigModel;
                return new BLL.Models.Profile.ProfilesConfigModel
                {
                    Filter = data.Filter,
                    PageNumber = data.PageNumber,
                    PageSize = data.PageSize,
                    SortState = (BLL.Models.Profile.ProfilesSortState)data.SortState
                } as TOut;
            }

            if (input.GetType() == typeof(BLL.Models.Profile.SearchResult))
            {
                var data = input as BLL.Models.Profile.SearchResult;
                return new ProfilesViewModel
                {
                    Profiles = data.Profiles.Select(x => new Profile
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Birthday = x.Birthday
                    }),
                    PageInfo = new Models.CollectionInfo
                    {
                        PageNumber = data.PageInfo.PageNumber,
                        PageSize = data.PageInfo.PageSize,
                        TotalItems = data.PageInfo.TotalItems
                    }
                } as TOut;
            }

            return default;
        }

        [RequestsCountRestrictor(count: 1)]
        public ActionResult Index(ProfilesConfigModel configModel)
        {
            var viewModel = _profileService.Search(
                To<ProfilesConfigModel, BLL.Models.Profile.ProfilesConfigModel>(configModel),
                _path
            );

            ViewData["SortState"] = configModel.SortState;
            ViewData["Filter"] = configModel.Filter;

            return View("Views/Profiles/Profiles.cshtml", To<BLL.Models.Profile.SearchResult, ProfilesViewModel>(viewModel));
        }
    }
}