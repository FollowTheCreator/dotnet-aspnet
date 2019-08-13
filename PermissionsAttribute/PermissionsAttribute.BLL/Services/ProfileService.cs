using PermissionsAttribute.BLL.Models;
using PermissionsAttribute.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsAttribute.BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repository;

        public ProfileService(IProfileRepository repository)
        {
            _repository = repository;
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
            await _repository.CreateAsync(convertedModel);
        }

        public async Task UpdateAsync(Profile profile)
        {
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _repository.IsEmailExistsAsync(email);
        }

        public async Task<Profile> RegisterProfileAsync(RegisterModel profile)
        {
            var registeredProfile = await _repository.RegisterProfileAsync(Utils.Convert.To<RegisterModel, DAL.Models.Profile>(profile));

            return Utils.Convert.To<DAL.Models.Profile, Profile>(registeredProfile);
        }

        public async Task<ProfilePermission> GetPermissionsAsync(Profile profile)
        {
            var profilePermission = await _repository.GetPermissionsAsync(Utils.Convert.To<Profile, DAL.Models.Profile>(profile));

            return Utils.Convert.To<DAL.Models.ProfilePermission, ProfilePermission>(profilePermission);
        }

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            return Utils.Convert.To<DAL.Models.Role, Role>(await _repository.GetRoleByNameAsync(name));
        }
    }
}
