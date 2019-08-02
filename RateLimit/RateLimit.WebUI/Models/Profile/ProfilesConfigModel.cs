using RateLimit.WebUI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RateLimit.WebUI.Models.Profile
{
    public class ProfilesConfigModel : IPageInfo, IFilterable, ISorterable
    {
        private int _pageNumber;

        private int _pageSize;

        public string Filter { get; set; }

        public int PageNumber
        {
            get
            {
                if (_pageNumber == 0)
                {
                    _pageNumber = 1;

                    return _pageNumber;
                }

                return _pageNumber;
            }
            set
            {
                _pageNumber = value;
            }
        }

        public int PageSize
        {
            get
            {
                if (_pageSize == 0)
                {
                    _pageSize = 4;

                    return _pageSize;
                }

                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        public ProfilesSortState SortState { get; set; }
    }
}
