using System.Collections.Generic;

namespace RateLimit.BLL.Models.Interfaces
{
    interface IProfilesCollection
    {
        IEnumerable<DAL.Models.Profile> Profiles { get; set; }
    }
}
