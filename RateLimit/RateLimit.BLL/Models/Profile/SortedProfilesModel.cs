using RateLimit.BLL.Models.Interfaces;
using System.Collections.Generic;

namespace RateLimit.BLL.Models.Profile
{
    class SortedProfilesModel : IProfilesCollection, IPageInfo, ISortable
    {
        public IEnumerable<Profile> Profiles { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public ProfilesSort SortState { get; set; }
    }
}
