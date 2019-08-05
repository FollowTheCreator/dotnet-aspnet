namespace RateLimit.DAL.Models.JsonProfileReposotiry
{
    public class ProfileSearchFilter
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public ProfilesSort SortState { get; set; }
    }
}
