using PermissionsAttribute.BLL.Models;
using PermissionsAttribute.DAL.Repositories;
using System.Threading.Tasks;
using Utils;

namespace PermissionsAttribute.BLL.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IProfileRepository _repository;

        public AccountService(IProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProfilePermission> GetPermissionsAsync(Profile profile)
        {
            var profilePermission = await _repository.GetPermissionsAsync(Utils.Convert.To<Profile, DAL.Models.Profile>(profile));

            return Utils.Convert.To<DAL.Models.ProfilePermission, ProfilePermission>(profilePermission);
        }

        public async Task<ProfilePermission> LogIn(Profile profile)
        {
            profile.PasswordHash = Base64Coder.ComputeSha256Hash(profile.PasswordHash);

            var permission = await GetPermissionsAsync(profile);

            return permission;
        }

        public async Task<ProfilePermission> RegisterProfileAsync(RegisterModel model)
        {
            if (!await IsEmailExistsAsync(model.Email))
            {
                var convertedModel = Utils.Convert.To<RegisterModel, DAL.Models.Profile>(model);

                convertedModel.Role = await _repository.GetRoleByNameAsync("user");
                convertedModel.PasswordHash = Base64Coder.ComputeSha256Hash(convertedModel.PasswordHash);

                var registeredProfile = await _repository.RegisterProfileAsync(convertedModel);

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
