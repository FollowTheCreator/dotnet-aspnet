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
            return await _context.Profile.Include(profile => profile.Role).ToListAsync();
        }

        public async Task<Profile> GetByIdAsync(int id)
        {
            return await _context.Profile.Where(x => x.Id == id).Include(profile => profile.Role).FirstOrDefaultAsync();
        }

        public async Task<ProfilePermission> GetPermissionsAsync(Profile profile)
        {
            var currentProfile = await _context
                    .Profile
                    .FirstOrDefaultAsync(p => p.Email == profile.Email && p.PasswordHash == profile.PasswordHash);

            var permissionNames = await _context
                .RolePermission
                .Where(rp => rp.RoleId == currentProfile.RoleId)
                .Select(rp => rp.Permission.Name)
                .ToListAsync();

            return new ProfilePermission()
            {
                PermissionNames = permissionNames,
                Id = currentProfile.Id
            };
        }

        public async Task<bool> IsEmailExistsAsync(string email)
        {
            Profile user = await _context.Profile.FirstOrDefaultAsync(u => u.Email == email);

            return user != null;
        }

        public async Task<Profile> RegisterProfileAsync(RegisterModel model)
        {
            Profile user = new Profile { Name = model.Name, Email = model.Email, PasswordHash = model.Password };
            user.Role = await GetRoleByNameAsync("user");

            await CreateAsync(user);

            return user;
        }

        public async Task UpdateAsync(Profile profile)
        {
            _context.Entry(profile).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            return await _context.Role.FirstOrDefaultAsync(r => r.Name == name);
        }
    }
}
