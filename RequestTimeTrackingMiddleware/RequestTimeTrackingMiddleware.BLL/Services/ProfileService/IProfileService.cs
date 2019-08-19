using RequestTimeTrackingMiddleware.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RequestTimeTrackingMiddleware.BLL.Services.ProfileService
{
    public interface IProfileService
    {
        Task<Profile> GetByIdAsync(int id);

        Task<IEnumerable<Profile>> GetAllAsync();

        Task CreateAsync(Profile profile);

        Task UpdateAsync(Profile profile);

        Task DeleteAsync(int id);
    }
}
