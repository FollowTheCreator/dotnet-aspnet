using RateLimit.BLL.Models.Profile;

namespace RateLimit.BLL.Models.Interfaces
{
    interface ISorterable
    {
        ProfilesSortState SortState { get; set; }
    }
}
