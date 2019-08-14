using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PermissionsAttribute.BLL.Services.ProfileService;
using PermissionsAttribute.WebUI.Attributes.PermissionAttribute;
using PermissionsAttribute.WebUI.Models;
using PermissionsAttribute.WebUI.Models.ViewModels;

namespace PermissionsAttribute.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _service;

        public ProfileController(IProfileService service)
        {
            _service = service;
        }

        public async Task<ActionResult<IEnumerable<ProfileViewModel>>> GetAllProfiles()
        {
            var profiles = await _service.GetAllAsync();

            var convertedProfiles = profiles.Select(x => Utils.Convert.To<BLL.Models.BLLProfile, ProfileViewModel>(x)).ToList();

            return View("~/Views/Profile/Profiles.cshtml", convertedProfiles);
        }

        [HasPermission(Permissions.GetProfileById)]
        public async Task<ActionResult<ProfileViewModel>> GetProfileById(int id)
        {
            var profile = await _service.GetByIdAsync(id);

            var convertedProfile = Utils.Convert.To<BLL.Models.BLLProfile, ProfileViewModel>(profile);

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

            var convertedModel = Utils.Convert.To<AddProfileModel, BLL.Models.AddProfileModel>(model);
            if (await _service.AddProfileAsync(convertedModel))
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
            var profile = await _service.GetByIdAsync(id);

            var convertedProfile = Utils.Convert.To<BLL.Models.BLLProfile, UpdateProfileModel>(profile);

            return View("~/Views/Profile/Update.cshtml", convertedProfile);
        }

        [HttpPost]
        [HasPermission(Permissions.UpdateProfile)]
        public async Task<ActionResult<Profile>> UpdateProfile(UpdateProfileModel model)
        {
            var convertedProfile = Utils.Convert.To<UpdateProfileModel, BLL.Models.Profile>(model);

            await _service.UpdateAsync(convertedProfile);

            return RedirectToAction("GetProfileById", "Profile", new { id = model.Id });
        }

        [HasPermission(Permissions.DeleteProfile)]
        public async Task<ActionResult> DeleteProfile(int id)
        {
            if (_service.IsCurrentUser(id))
            {
                return RedirectToAction("GetProfileById", "Profile", new { id = id });
            }

            await _service.DeleteAsync(id);
            return RedirectToAction("GetAllProfiles", "Profile");
        }
    }
}