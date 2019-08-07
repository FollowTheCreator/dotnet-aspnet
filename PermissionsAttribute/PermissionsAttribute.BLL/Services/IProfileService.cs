using PermissionsAttribute.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PermissionsAttribute.BLL.Services
{
    public interface IProfileService
    {
        Task<Profile> GetByIdAsync(int id);

        Task<IEnumerable<Profile>> GetAllAsync();

        Task CreateAsync(Profile item);

        Task UpdateAsync(Profile item);

        Task DeleteAsync(int id);
    }
}
