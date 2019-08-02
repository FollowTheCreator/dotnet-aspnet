namespace RateLimit.WebUI.Models.Profile
{
    public class ProfilesConfigModel
    {
        public string Filter { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public ProfilesSortState SortState { get; set; }
    }
}
