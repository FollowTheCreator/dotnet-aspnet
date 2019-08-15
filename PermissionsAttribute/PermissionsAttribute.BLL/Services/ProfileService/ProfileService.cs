using Microsoft.Extensions.Configuration;
using PermissionsAttribute.BLL.Models.ProfileModels;
using PermissionsAttribute.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace PermissionsAttribute.BLL.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly IConfiguration _config;

        private readonly IProfileRepository _repository;

        public ProfileService(IProfileRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
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
            convertedModel.Role = await _repository.GetRoleByNameAsync(
                _config.GetSection("AppSettings:DefaultUserRole").Value
            );

            await _repository.CreateAsync(convertedModel);
        }

        public async Task<bool> AddProfileAsync(AddProfileModel model)
        {
            if (!await IsEmailExistsAsync(model.Email))
            {
                model.Password = Coder.Encode(model.Password);

                await CreateAsync(model);

                return true;
            }

            return false;
        }

        public async Task UpdateAsync(UpdateProfileModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                var oldProfile = await _repository.GetByIdAsync(model.Id);
                model.Password = oldProfile.PasswordHash;
            }
            else
            {
                model.Password = Coder.Encode(model.Password);
            }

            var profile = Utils.Convert.To<UpdateProfileModel, Profile>(model);

            var role = await _repository.GetRoleByNameAsync(profile.Role.Name);
            profile.RoleId = role.Id;

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
    }
}
