using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PermissionsAttribute.BLL.Services;
using PermissionsAttribute.WebUI.Models;

namespace PermissionsAttribute.WebUI.Controllers
{
    [Route("Profiles")]
    public class ProfileController : Controller
    {
        private readonly IProfileService _service;

        public ProfileController(IProfileService service)
        {
            _service = service;
        }

        [Route("")]
        public async Task<ActionResult<IEnumerable<Profile>>> GetAllProfiles()
        {
            var profiles = await _service.GetAllAsync();

            var convertedProfiles = profiles.Select(x => Utils.Convert.To<BLL.Models.Profile, Profile>(x)).ToList();

            return convertedProfiles;
        }

        [Route("{id}")]
        public async Task<ActionResult<Profile>> GetProfileById(int id)
        {
            var profile = await _service.GetByIdAsync(id);

            var convertedProfile = Utils.Convert.To<BLL.Models.Profile, Profile>(profile);

            return convertedProfile;
        }
        [NonAction]
        public async Task<ActionResult<string>> AddProfile(Profile profile)
        {
            return $"Profile (Id: {profile.Id}) was created.";
        }
        [NonAction]
        public async Task<ActionResult<string>> UpdateProfile(Profile profile)
        {
            return $"Profile (Id: {profile.Id}) was updated.";
        }
        [NonAction]
        public async Task<ActionResult<string>> DeleteProfile(int id)
        {
            return $"Profile (Id: {id}) was deleted.";
        }
    }
}