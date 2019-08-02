using Newtonsoft.Json;
using RateLimit.DAL.Models;
using RateLimit.WebUI.Models.Profile;
using System;
using System.Linq;

namespace RateLimit.WebUI.Utils
{
    public class Convert
    {
        /// <summary>
        /// only for equal objects
        /// </summary>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        [Obsolete("only for equal objects")]
        public static TOut To<TOut>(object input)
        {
            return JsonConvert.DeserializeObject<TOut>(JsonConvert.SerializeObject(input));
        }

        public static TOut To<TIn, TOut>(TIn input) where TOut : class
        {
            if (input.GetType() == typeof(ProfilesConfigModel))
            {
                var data = input as ProfilesConfigModel;
                return new BLL.Models.Profile.ProfilesConfigModel
                {
                    Filter = data.Filter,
                    PageNumber = data.PageNumber,
                    PageSize = data.PageSize,
                    SortState = (BLL.Models.Profile.ProfilesSortState)data.SortState
                } as TOut;
            }

            if (input.GetType() == typeof(BLL.Models.Profile.SearchResult))
            {
                var data = input as BLL.Models.Profile.SearchResult;
                return new ProfilesViewModel
                {
                    Profiles = data.Profiles.Select(x => new Profile
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Birthday = x.Birthday
                    }),
                    PageInfo = new Models.CollectionInfo
                    {
                        PageNumber = data.PageInfo.PageNumber,
                        PageSize = data.PageInfo.PageSize,
                        TotalItems = data.PageInfo.TotalItems,
                        TotalPages = data.PageInfo.TotalPages
                    }
                } as TOut;
            }

            return default;
        }
    }
}
