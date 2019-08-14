using Microsoft.AspNetCore.Http;
using PermissionsAttribute.BLL.Models;
using PermissionsAttribute.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace PermissionsAttribute.BLL.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repository;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileService(IProfileRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BLLProfile> GetByIdAsync(int id)
        {
            var profile = await _repository.GetByIdAsync(id);

            var convertedProfile = Utils.Convert.To<DAL.Models.Profile, BLLProfile>(profile);

            return convertedProfile;
        }

        public async Task<IEnumerable<BLLProfile>> GetAllAsync()
        {
            var profiles = await _repository.GetAllAsync();

            var convertedProfiles = profiles.Select(x => Utils.Convert.To<DAL.Models.Profile, BLLProfile>(x)).ToList();

            return convertedProfiles;
        }

        public async Task CreateAsync(AddProfileModel model)
        {
            var convertedModel = Utils.Convert.To<AddProfileModel, DAL.Models.Profile>(model);
            convertedModel.Role = await _repository.GetRoleByNameAsync("user");

            await _repository.CreateAsync(convertedModel);
        }

        public async Task<bool> AddProfileAsync(AddProfileModel model)
        {
            if (!await IsEmailExistsAsync(model.Email))
            {
                model.Password = Base64Coder.ComputeSha256Hash(model.Password);

                await CreateAsync(model);

                return true;
            }

            return false;
        }

        public async Task UpdateAsync(Profile profile)
        {
            var role = await _repository.GetRoleByNameAsync(profile.Role.Name);
            profile.RoleId = role.Id;

            if (string.IsNullOrWhiteSpace(profile.PasswordHash))
            {
                var oldProfile = await _repository.GetByIdAsync(profile.Id);
                profile.PasswordHash = oldProfile.PasswordHash;
            }
            profile.PasswordHash = Base64Coder.ComputeSha256Hash(profile.PasswordHash);

            var convertedProfile = Utils.Convert.To<Profile, DAL.Models.Profile>(profile);

            await _repository.UpdateAsync(convertedProfile);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _repository.IsEmailExistsAsync(email);
        }

        public async Task<ProfilePermission> GetPermissionsAsync(Profile profile)
        {
            var profilePermission = await _repository.GetPermissionsAsync(Utils.Convert.To<Profile, DAL.Models.Profile>(profile));

            return Utils.Convert.To<DAL.Models.ProfilePermission, ProfilePermission>(profilePermission);
        }

        public bool IsCurrentUser(int id)
        {
            var currentId = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .FirstOrDefault(c => c.Type == "id")
                .Value;

            if (id.ToString() == currentId)
            {
                return true;
            }

            return false;
        }
    }
}
