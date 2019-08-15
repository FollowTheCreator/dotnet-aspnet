using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PermissionsAttribute.BLL.Services.ClaimService;
using PermissionsAttribute.BLL.Services.ProfileService;
using PermissionsAttribute.WebUI.Attributes.PermissionAttribute;
using PermissionsAttribute.WebUI.Models;
using PermissionsAttribute.WebUI.Models.ViewModels;

namespace PermissionsAttribute.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        private readonly IClaimService _claimService;

        public ProfileController(IProfileService profileService, IClaimService claimService)
        {
            _profileService = profileService;
            _claimService = claimService;
        }

        public async Task<ActionResult<IEnumerable<ProfileViewModel>>> GetAllProfiles()
        {
            var profiles = await _profileService.GetAllAsync();

            var convertedProfiles = Utils.Convert.To<BLL.Models.ProfileModels.BLLProfile, ProfileViewModel>(profiles);

            return View("~/Views/Profile/Profiles.cshtml", convertedProfiles);
        }

        [HasPermission(Permissions.GetProfileById)]
        public async Task<ActionResult<ProfileViewModel>> GetProfileById(int id)
        {
            var profile = await _profileService.GetByIdAsync(id);

            var convertedProfile = Utils.Convert.To<BLL.Models.ProfileModels.BLLProfile, ProfileViewModel>(profile);

            return View("~/Views/Profile/Profile.cshtml", convertedProfile);
        }

        [HttpGet]
        [HasPermission(Permissions.AddProfile)]
        public ActionResult AddProfile()
        {
            return View("~/Views/Profile/Add.cshtml");
        }

        [HttpPost]
        [HasPermission(Permissions.AddProfile)]
        public async Task<ActionResult<string>> AddProfile(AddProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("~/Views/Profile/Add.cshtml", model);
            }

            var convertedModel = Utils.Convert.To<AddProfileModel, BLL.Models.ProfileModels.AddProfileModel>(model);
            if (await _profileService.AddProfileAsync(convertedModel))
            {
                return RedirectToAction("GetAllProfiles", "Profile");
            }

            ModelState.AddModelError("", "This Email already exists");
            return View("~/Views/Profile/Add.cshtml", model);
        }

        [HttpGet]
        [HasPermission(Permissions.UpdateProfile)]
        public async Task<ActionResult<UpdateProfileModel>> UpdateProfile(int id)
        {
            var profile = await _profileService.GetByIdAsync(id);

            var convertedProfile = Utils.Convert.To<BLL.Models.ProfileModels.BLLProfile, UpdateProfileModel>(profile);

            return View("~/Views/Profile/Update.cshtml", convertedProfile);
        }

        [HttpPost]
        [HasPermission(Permissions.UpdateProfile)]
        public async Task<ActionResult<Profile>> UpdateProfile(UpdateProfileModel model)
        {
            var convertedModel = Utils.Convert.To<UpdateProfileModel, BLL.Models.ProfileModels.UpdateProfileModel>(model);

            await _profileService.UpdateAsync(convertedModel);

            return RedirectToAction("GetProfileById", "Profile", new { id = model.Id });
        }

        [HasPermission(Permissions.DeleteProfile)]
        public async Task<ActionResult> DeleteProfile(int id)
        {
            if (_claimService.IsCurrentUser(id))
            {
                return RedirectToAction("GetProfileById", "Profile", new { id = id });
            }

            await _profileService.DeleteAsync(id);
            return RedirectToAction("GetAllProfiles", "Profile");
        }
    }
}