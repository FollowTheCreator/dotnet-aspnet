using System.Collections.Generic;

namespace RateLimit.WebUI.Models.Profile
{
    public class ProfilesViewModel
    {
        public IEnumerable<DAL.Models.Profile> Profiles { get; set; }

        public CollectionInfo PageInfo { get; set; }
    }
}
