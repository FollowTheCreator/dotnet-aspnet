using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RequestTimeTrackingMiddleware.BLL.Services.ProfileService;
using RequestTimeTrackingMiddleware.WebUI.Models;

namespace RequestTimeTrackingMiddleware.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public async Task<ActionResult<IEnumerable<Profile>>> GetAllProfiles()
        {
            var profiles = await _profileService.GetAllAsync();

            var convertedProfiles = Utils.Convert.To<BLL.Models.Profile, Profile>(profiles);

            return View("~/Views/Profile/Profiles.cshtml", convertedProfiles);
        }

        public async Task<ActionResult<Profile>> GetProfileById(int id)
        {
            var profile = await _profileService.GetByIdAsync(id);

            var convertedProfile = Utils.Convert.To<BLL.Models.Profile, Profile>(profile);

            return View("~/Views/Profile/Profile.cshtml", convertedProfile);
        }

        [HttpGet]
        public ActionResult AddProfile()
        {
            return View("~/Views/Profile/Add.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> AddProfile(Profile profile)
        {
            var convertedProfile = Utils.Convert.To<Profile, BLL.Models.Profile>(profile);

            await _profileService.CreateAsync(convertedProfile);
                
            return RedirectToAction("GetAllProfiles", "Profile");
        }

        [HttpGet]
        public async Task<ActionResult<Profile>> UpdateProfile(int id)
        {
            var profile = await _profileService.GetByIdAsync(id);

            var convertedProfile = Utils.Convert.To<BLL.Models.Profile, Profile>(profile);

            return View("~/Views/Profile/Update.cshtml", convertedProfile);
        }

        [HttpPost]
        public async Task<ActionResult<Profile>> UpdateProfile(Profile profile)
        {
            var convertedProfile = Utils.Convert.To<Profile, BLL.Models.Profile>(profile);

            await _profileService.UpdateAsync(convertedProfile);

            return RedirectToAction("GetProfileById", "Profile", new { id = profile.Id });
        }

        public async Task<ActionResult> DeleteProfile(int id)
        {
            await _profileService.DeleteAsync(id);
            return RedirectToAction("GetAllProfiles", "Profile");
        }
    }
}