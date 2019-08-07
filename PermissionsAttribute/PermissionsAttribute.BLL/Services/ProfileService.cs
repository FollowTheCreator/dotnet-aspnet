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

        public async Task<Profile> GetByIdAsync(int id)
        {
            var profile = await _repository.GetByIdAsync(id);

            var convertedProfile = Utils.Convert.To<DAL.Models.Profile, Profile>(profile);

            return convertedProfile;
        }

        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            var profiles = await _repository.GetAllAsync();

            var convertedProfiles = profiles.Select(x => Utils.Convert.To<DAL.Models.Profile, Profile>(x)).ToList();

            return convertedProfiles;
        }

        public async Task CreateAsync(Profile item)
        {

        }

        public async Task UpdateAsync(Profile item)
        {

        }

        public async Task DeleteAsync(int id)
        {

        }
    }
}
