using RateLimit.BLL.Models.Interfaces;

namespace RateLimit.BLL.Models.Profile
{
    public class ProfilesSearchRequest : IPageInfo, IFilterable, ISortable
    { 
        public string Filter { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public ProfilesSort SortState { get; set; }
    }
}
