using System.Collections.Generic;

namespace RateLimit.WebUI.Models.Interfaces
{
    interface IProfilesCollection
    {
        IEnumerable<Profile.Profile> Profiles { get; set; }
    }
}
