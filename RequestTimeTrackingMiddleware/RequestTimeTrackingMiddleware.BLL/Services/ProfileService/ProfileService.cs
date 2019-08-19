using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RequestTimeTrackingMiddleware.BLL.Models;
using RequestTimeTrackingMiddleware.DAL.Repositories.ProfileRepository;

namespace RequestTimeTrackingMiddleware.BLL.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repository;

        public ProfileService(IProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Profile profile)
        {
            var convertedProfile = Utils.Convert.To<Profile, DAL.Models.Profile>(profile);
            await _repository.CreateAsync(convertedProfile);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            var profiles = await _repository.GetAllAsync();

            var convertedProfiles = Utils.Convert.To<DAL.Models.Profile, Profile>(profiles);

            return convertedProfiles;
        }

        public async Task<Profile> GetByIdAsync(int id)
        {
            var profile = await _repository.GetByIdAsync(id);

            var convertedProfile = Utils.Convert.To<DAL.Models.Profile, Profile>(profile);

            return convertedProfile;
        }

        public async Task UpdateAsync(Profile profile)
        {
            var convertedProfile = Utils.Convert.To<Profile, DAL.Models.Profile>(profile);

            await _repository.UpdateAsync(convertedProfile);
        }
    }
}
