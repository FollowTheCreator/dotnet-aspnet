using System.Collections.Generic;

namespace RateLimit.WebUI.Models.Interfaces
{
    interface IProfilesCollection
    {
        IEnumerable<DAL.Models.Profile> Profiles { get; set; }
    }
}
