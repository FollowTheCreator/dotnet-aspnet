using PermissionsAttribute.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PermissionsAttribute.BLL.Services.ProfileService
{
    public interface IProfileService
    {
        Task<BLLProfile> GetByIdAsync(int id);

        Task<IEnumerable<BLLProfile>> GetAllAsync();

        Task CreateAsync(AddProfileModel profile);

        Task UpdateAsync(Profile profile);

        Task DeleteAsync(int id);

        Task<bool> IsEmailExistsAsync(string email);

        Task<ProfilePermission> GetPermissionsAsync(Profile profile);

        Task<bool> AddProfileAsync(AddProfileModel profile);

        bool IsCurrentUser(int id);
    }
}
