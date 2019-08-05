using JsonFormatter.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JsonFormatter.DAL.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ProfilesDbContext _context;

        public ProfileRepository(ProfilesDbContext context)
        {
            _context = context;
        }

        public async Task<Profile> GetByIdAsync(int id)
        {
            return await _context
                .Profiles
                .FirstOrDefaultAsync(profile => profile.Id == id);
        }
    }
}
