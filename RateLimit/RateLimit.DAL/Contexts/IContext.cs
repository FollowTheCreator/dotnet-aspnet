using RateLimit.DAL.Models;
using System.Collections.Generic;

namespace RateLimit.DAL.Contexts
{
    public interface IContext
    {
        IEnumerable<Profile> Profiles { get; set; }
    }
}
