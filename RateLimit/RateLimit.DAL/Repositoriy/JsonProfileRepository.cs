using RateLimit.DAL.Contexts;
using RateLimit.DAL.Models;
using RateLimit.DAL.Models.JsonProfileReposotiry;
using System;
using System.Collections.Generic;
using System.Linq;
using Utils.Extensions;

namespace RateLimit.DAL.Repositoriy
{
    public class JsonProfileRepository : IProfileRepository
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

        public int Count(ProfileFilter filterable)
        {
            if(string.IsNullOrEmpty(filterable.Filter))
            {
                return _context.Profiles.Count();
            }

            return _context.Profiles.Where(profile =>
                profile.Id.ToString().Contains(filterable.Filter) ||
                profile.FirstName.Contains(filterable.Filter) ||
                profile.LastName.Contains(filterable.Filter) ||
                profile.Birthday.ToBirthdayString().Contains(filterable.Filter)
            )
            .Count();
        }

        public IEnumerable<Profile> Search(ProfileSearchFilter filter)
        {
            var profiles = _context.Profiles;

            switch (filter.SortState)
            {
                case ProfilesSort.Id:
                    profiles = profiles.OrderBy(profile => profile.Id);
                    break;
                case ProfilesSort.FirstName:
                    profiles = profiles.OrderBy(profile => profile.FirstName);
                    break;
                case ProfilesSort.LastName:
                    profiles = profiles.OrderBy(profile => profile.LastName);
                    break;
                default:
                    profiles = profiles.OrderBy(profile => profile.Birthday);
                    break;
            }

            return profiles
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);
        }
    }
}
