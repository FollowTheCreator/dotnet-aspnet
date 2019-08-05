using RateLimit.BLL.Models.Interfaces;
using System.Collections.Generic;

namespace RateLimit.BLL.Models.Profile
{
    public class SearchResult : IProfilesCollection
    {
        public IEnumerable<Profile> Profiles { get; set; }

        public CollectionInfo PageInfo { get; set; }
    }
}
