namespace RateLimit.BLL.Models.Interfaces
{
    interface ICollectionInfo : IPageInfo
    {
        int TotalItems { get; set; }

        int GetTotalPages();
    }
}
