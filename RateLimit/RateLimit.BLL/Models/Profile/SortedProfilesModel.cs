using RateLimit.BLL.Models.Interfaces;
using System.Collections.Generic;

namespace RateLimit.BLL.Models.Profile
{
    public class SortedProfilesModel : IProfilesCollection, IPageInfo, ISorterable
    {
        public IEnumerable<Profile> Profiles { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public ProfilesSortState SortState { get; set; }
    }
}
