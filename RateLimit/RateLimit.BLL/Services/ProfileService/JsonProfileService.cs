using System.Collections.Generic;
using System.Linq;
using System.IO;
using RateLimit.BLL.Models;
using RateLimit.BLL.Models.Profile;
using Newtonsoft.Json;
using Utils.Extensions;
using RateLimit.DAL.Contexts;
using RateLimit.DAL.Models;

namespace RateLimit.BLL.Services.ProfileService
{
    public class JsonProfileService : IProfileService
    {
        public SearchResult Search(ProfilesConfigModel actionModel, string path)
        {
            var filteredCollection = Filter(LoadFile(path), actionModel.Filter);

            return new SearchResult
            {
                Profiles = GetCollection(new SortedProfilesModel
                {
                    Profiles = filteredCollection,
                    PageNumber = actionModel.PageNumber,
                    PageSize = actionModel.PageSize,
                    SortState = actionModel.SortState
                }),
                PageInfo = new CollectionInfo
                {
                    PageNumber = actionModel.PageNumber,
                    PageSize = actionModel.PageSize,
                    TotalItems = filteredCollection.Count()
                }
            };
        }

        private IEnumerable<Profile> LoadFile(string path)
        {
            JsonContext context = new JsonContext(path);

            return context.Profiles;
        }

        private IEnumerable<Profile> Filter(IEnumerable<Profile> data, string filter)
        {
            if (string.IsNullOrEmpty(filter) || filter == "")
            {
                return data;
            }

            return data.Where(profile =>
                profile.Id.ToString().Contains(filter) ||
                profile.FirstName.Contains(filter) ||
                profile.LastName.Contains(filter) ||
                profile.Birthday.ToBirthdayString().Contains(filter)
            );
        }

        private IEnumerable<Profile> GetCollection(SortedProfilesModel sortedProfilesModel)
        {
            var profiles = sortedProfilesModel.Profiles;

            switch (sortedProfilesModel.SortState)
            {
                case ProfilesSortState.Id:
                    profiles = profiles.OrderBy(profile => profile.Id);
                    break;
                case ProfilesSortState.FirstName:
                    profiles = profiles.OrderBy(profile => profile.FirstName);
                    break;
                case ProfilesSortState.LastName:
                    profiles = profiles.OrderBy(profile => profile.LastName);
                    break;
                default:
                    profiles = profiles.OrderBy(profile => profile.Birthday);
                    break;
            }

            profiles = profiles
                .Skip((sortedProfilesModel.PageNumber - 1) * sortedProfilesModel.PageSize)
                .Take(sortedProfilesModel.PageSize);

            //Thread.Sleep(5000);

            return profiles;
        }
    }
}
