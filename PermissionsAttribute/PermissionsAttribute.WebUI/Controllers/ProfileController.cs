using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionsAttribute.BLL.Services;
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

            return View("Views/Profile/Profiles.cshtml", convertedProfiles);
        }

        [HasPermission(Permissions.GetProfileById)]
        public async Task<ActionResult<ProfileViewModel>> GetProfileById(int id)
        {
            var profile = await _service.GetByIdAsync(id);

            var convertedProfile = Utils.Convert.To<BLL.Models.BLLProfile, ProfileViewModel>(profile);

            return View("Views/Profile/Profile.cshtml", convertedProfile);
        }

        public async Task<ActionResult<string>> AddProfile(Profile profile)
        {
            return $"Profile (Id: {profile.Id}) was created.";
        }

        public async Task<ActionResult> UpdateProfile(int id)
        {
            var updateTarget = await _service.GetByIdAsync(id);

            return View("Views/Profile/Update.cshtml");
        }

        [HasPermission(Permissions.DeleteProfile)]
        public async Task<ActionResult> DeleteProfile(int id)
        {
            if(id.ToString() == User.Claims.FirstOrDefault(c => c.Type == "id").Value)
            {
                return RedirectToAction("GetProfileById", "Profile", new { id = id });
            }
            await _service.DeleteAsync(id);

            return RedirectToAction("GetAllProfiles", "Profile");
        }
    }
}