using PermissionsAttribute.BLL.Models.ProfileModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PermissionsAttribute.BLL.Services.ProfileService
{
    public interface IProfileService
    {
        Task<BLLProfile> GetByIdAsync(int id);

        Task<IEnumerable<BLLProfile>> GetAllAsync();

        Task CreateAsync(AddProfileModel profile);

        Task UpdateAsync(UpdateProfileModel profile);

        Task DeleteAsync(int id);

        Task<bool> IsEmailExistsAsync(string email);

        Task<bool> AddProfileAsync(AddProfileModel profile);
    }
}
