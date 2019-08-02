using RateLimit.BLL.Models.Interfaces;

namespace RateLimit.BLL.Models.Profile
{
    public class ProfilesConfigModel : IPageInfo, IFilterable, ISorterable
    { 
        public string Filter { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public ProfilesSortState SortState { get; set; }
    }
}
