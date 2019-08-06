using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PermissionsAttribute.DAL.Repositories;
using PermissionsAttribute.WebUI.Models;

namespace PermissionsAttribute.WebUI.Controllers
{
    [Route("Profiles")]
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _repository;

        public ProfileController(IProfileRepository repository)
        {
            _repository = repository;
        }

        [Route("")]
        public async Task<ActionResult<IEnumerable<Profile>>> GetAllProfiles()
        {
            var profiles = await _repository.GetAllAsync();

            var convertedProfiles = new List<Profile>();
            foreach(var profile in profiles)
            {
                convertedProfiles.Add(Utils.Convert.To<DAL.Models.Profile, Profile>(profile));
            }

            return convertedProfiles;
        }

        [Route("{id}")]
        public async Task<ActionResult<Profile>> GetProfileById(int id)
        {
            var profile = await _repository.GetByIdAsync(id);

            var convertedProfile = Utils.Convert.To<DAL.Models.Profile, Profile>(profile);

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