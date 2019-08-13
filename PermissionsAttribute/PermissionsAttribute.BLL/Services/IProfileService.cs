using PermissionsAttribute.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsAttribute.BLL.Services
{
    public interface IProfileService
    {
        Task<BLLProfile> GetByIdAsync(int id);

        Task<IEnumerable<BLLProfile>> GetAllAsync();

        Task CreateAsync(AddProfileModel profile);

        Task UpdateAsync(Profile profile);

        Task DeleteAsync(int id);

        Task<bool> IsEmailExistsAsync(string email);

        Task<Profile> RegisterProfileAsync(RegisterModel profile);

        Task<ProfilePermission> GetPermissionsAsync(Profile profile);

        Task<Role> GetRoleByNameAsync(string name);
    }
}
