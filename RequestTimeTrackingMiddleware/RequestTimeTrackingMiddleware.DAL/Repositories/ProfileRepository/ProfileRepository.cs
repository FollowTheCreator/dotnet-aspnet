using Microsoft.EntityFrameworkCore;
using RequestTimeTrackingMiddleware.DAL.Models;
using RequestTimeTrackingMiddleware.DAL.Models.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestTimeTrackingMiddleware.DAL.Repositories.ProfileRepository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly TimeTrackingMiddlewareDbContext _context;

        public ProfileRepository(TimeTrackingMiddlewareDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Profile profile)
        {
            _context.Profile.Add(profile);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _context.Profile.Remove(await GetByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            return await _context
                .Profile
                .ToListAsync();
        }

        public async Task<Profile> GetByIdAsync(int id)
        {
            return await _context
                .Profile
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Profile profile)
        {
            _context.Entry(profile).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
