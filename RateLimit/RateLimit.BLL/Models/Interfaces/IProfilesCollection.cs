using System.Collections.Generic;

namespace RateLimit.BLL.Models.Interfaces
{
    interface IProfilesCollection
    {
        IEnumerable<Profile.Profile> Profiles { get; set; }
    }
}
