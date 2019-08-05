using RateLimit.DAL.Models;
using RateLimit.DAL.Models.JsonProfileReposotiry;
using System.Collections.Generic;

namespace RateLimit.DAL.Repositoriy
{
    public interface IProfileRepository: IRepository<Profile>
    {
        int Count(ProfileFilter filterable);

        IEnumerable<Profile> Search(ProfileSearchFilter filter);
    }
}
