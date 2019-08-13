using PermissionsAttribute.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsAttribute.DAL.Repositories
{
    public interface IProfileRepository : IRepository<Profile>
    {
        Task<bool> IsEmailExistsAsync(string email);

        Task<Profile> RegisterProfileAsync(Profile profile);

        Task<ProfilePermission> GetPermissionsAsync(Profile profile);

        Task<Role> GetRoleByNameAsync(string name);
    }
}
