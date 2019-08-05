using RateLimit.BLL.Models.Interfaces;

namespace RateLimit.BLL.Models
{
    public class CollectionInfo : ICollectionInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
    }
}
