using RateLimit.WebUI.Models.Interfaces;
using System;

namespace RateLimit.WebUI.Models
{
    public class CollectionInfo : ICollectionInfo
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalItems { get; set; }

        public int GetTotalPages()
        {
            return (int)Math.Ceiling(TotalItems / (double)PageSize);
        }
    }
}
