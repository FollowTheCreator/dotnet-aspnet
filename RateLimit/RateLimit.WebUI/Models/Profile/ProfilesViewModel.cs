using System.Collections.Generic;

namespace RateLimit.WebUI.Models.Profile
{
    public class ProfilesViewModel
    {
        public IEnumerable<Profile> Profiles { get; set; }

        public CollectionInfo PageInfo { get; set; }
    }
}
