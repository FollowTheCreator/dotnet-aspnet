using RateLimit.DAL.Contexts;
using RateLimit.DAL.Models;
using System.Collections.Generic;

namespace RateLimit.DAL.Repositoriy
{
    public class JsonProfileRepository : IRepository<Profile>
    {
        private readonly IContext _context;

        public JsonProfileRepository(IContext context)
        {
            _context = context;
        }

        public IEnumerable<Profile> GetAll()
        {
            return _context.Profiles;
        }
    }
}
