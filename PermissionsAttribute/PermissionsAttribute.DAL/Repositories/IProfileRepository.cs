using PermissionsAttribute.DAL.Models;
using System.Threading.Tasks;

namespace PermissionsAttribute.DAL.Repositories
{
    public interface IProfileRepository : IRepository<Profile>
    {
        Task<bool> IsEmailExistsAsync(string email);

        Task<ProfilePermission> GetPermissionsAsync(Profile profile);

        Task<Role> GetRoleByNameAsync(string name);

        Task<Profile> GetCurrentProfile(Profile profile);
    }
}
