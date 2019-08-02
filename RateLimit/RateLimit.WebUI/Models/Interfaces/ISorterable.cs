using RateLimit.WebUI.Models.Profile;

namespace RateLimit.WebUI.Models.Interfaces
{
    interface ISorterable
    {
        ProfilesSortState SortState { get; set; }
    }
}
