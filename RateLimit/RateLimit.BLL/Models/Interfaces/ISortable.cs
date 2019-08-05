using RateLimit.BLL.Models.Profile;

namespace RateLimit.BLL.Models.Interfaces
{
    interface ISortable
    {
        ProfilesSort SortState { get; set; }
    }
}
