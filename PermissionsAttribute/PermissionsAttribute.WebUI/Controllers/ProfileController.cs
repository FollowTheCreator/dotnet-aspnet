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
                return View(model);
            }

            if (!await _service.IsEmailExistsAsync(model.Email))
            {
                var convertedModel = Utils.Convert.To<AddProfileModel, BLL.Models.AddProfileModel>(model);
                await _service.CreateAsync(convertedModel);

                return RedirectToAction("GetAllProfiles", "Profile");
            }
            else
            {
                ModelState.AddModelError("", "This Email already exists");
            }

            return View(model);
        }

        [HttpGet]
        [HasPermission(Permissions.UpdateProfile)]
        public async Task<ActionResult> UpdateProfile(int id)
        {
            var updateTarget = await _service.GetByIdAsync(id);

            return View("~/Views/Profile/Update.cshtml");
        }

        [HttpPost]
        [HasPermission(Permissions.UpdateProfile)]
        public async Task<ActionResult> UpdateProfile(Profile profile)
        {
            var convertedProfile = Utils.Convert.To<Profile, BLL.Models.Profile>(profile);

            await _service.UpdateAsync(convertedProfile);

            return View("~/Views/Profile/Update.cshtml");
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