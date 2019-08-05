namespace RateLimit.BLL.Models.Interfaces
{
    interface IPageInfo
    {
        int PageNumber { get; set; }

        int PageSize { get; set; }
    }
}
