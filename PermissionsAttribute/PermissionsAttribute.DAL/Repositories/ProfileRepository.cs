using System.Collections.Generic;
using System.Threading.Tasks;
using PermissionsAttribute.DAL.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PermissionsAttribute.DAL.Models.Contexts;

namespace PermissionsAttribute.DAL.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly PermissionsDbContext _context;

        public ProfileRepository(PermissionsDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Profile item)
        {
            await _context.Profile.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            _context.Profile.Remove(await GetByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            return await _context.Profile.ToListAsync();
        }

        public async Task<Profile> GetByIdAsync(int id)
        {
            return await _context.Profile.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Profile item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
