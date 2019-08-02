using RateLimit.WebUI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimit.WebUI.Models.Profile
{
    public class ProfilesViewModel : IProfilesCollection
    {
        public IEnumerable<DAL.Models.Profile> Profiles { get; set; }

        public CollectionInfo PageInfo { get; set; }
    }
}
