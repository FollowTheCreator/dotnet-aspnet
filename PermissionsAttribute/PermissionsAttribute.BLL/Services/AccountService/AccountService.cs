using Microsoft.Extensions.Configuration;
using PermissionsAttribute.BLL.Models.Authentication;
using PermissionsAttribute.BLL.Models.ProfileModels;
using PermissionsAttribute.DAL.Repositories;
using System.Threading.Tasks;
using Utils;

namespace PermissionsAttribute.BLL.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _config;

        private readonly IProfileRepository _repository;

        public AccountService(IProfileRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<ProfilePermission> GetPermissionsAsync(Profile profile)
        {
            var currentProfile = await _repository.GetCurrentProfile(Utils.Convert.To<Profile, DAL.Models.Profile>(profile));
            if (currentProfile == null)
            {
                return default;
            }

            var profilePermission = await _repository.GetPermissionsAsync(currentProfile);

            return Utils.Convert.To<DAL.Models.ProfilePermission, ProfilePermission>(profilePermission);
        }

        public async Task<ProfilePermission> LogIn(LoginModel model)
        {
            model.Password = Coder.Encode(model.Password);

            var profile = Utils.Convert.To<LoginModel, Profile>(model);

            return await GetPermissionsAsync(profile);
        }

        public async Task<ProfilePermission> RegisterProfileAsync(RegisterModel model)
        {
            if (!await IsEmailExistsAsync(model.Email))
            {
                model.Password = Coder.Encode(model.Password);

                var convertedModel = Utils.Convert.To<RegisterModel, DAL.Models.Profile>(model);

                convertedModel.Role = await _repository.GetRoleByNameAsync(
                    _config.GetSection("AppSettings:DefaultUserRole").Value
                );

                await _repository.CreateAsync(convertedModel);

                var registeredProfile = await _repository.GetByIdAsync(convertedModel.Id);

                var convertedProfile = Utils.Convert.To<DAL.Models.Profile, Profile>(registeredProfile);

                return await GetPermissionsAsync(convertedProfile);
            }

            return default;
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _repository.IsEmailExistsAsync(email);
        }
    }
}
