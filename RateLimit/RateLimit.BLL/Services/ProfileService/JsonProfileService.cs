using System.Collections.Generic;
using System.Linq;
using RateLimit.BLL.Models;
using RateLimit.BLL.Models.Profile;
using Utils.Extensions;
using RateLimit.DAL.Models;
using RateLimit.DAL.Repositoriy;
using RateLimit.BLL.Models.Interfaces;

namespace RateLimit.BLL.Services.ProfileService
{
    public class JsonProfileService : IProfileService
    {
        private readonly IRepository<Profile> _profileRepository;

        public JsonProfileService(IRepository<Profile> profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public SearchResult Search(ProfilesConfigModel actionModel)
        {
            if(actionModel.PageNumber == 0)
            {
                actionModel.PageNumber = 1;
            }

            if(actionModel.PageSize == 0)
            {
                actionModel.PageSize = 4;
            }

            var filteredCollection = Filter(_profileRepository.GetAll(), actionModel);

            var filteredCollectionCount = filteredCollection.Count();
            var totalPages = (int)System.Math.Ceiling(filteredCollection.Count() / (double)actionModel.PageSize);

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
                    TotalItems = filteredCollectionCount,
                    TotalPages = totalPages
                }
            };
        }

        private IEnumerable<Profile> Filter(IEnumerable<Profile> data, IFilterable filterable)
        {
            if (string.IsNullOrEmpty(filterable.Filter))
            {
                return data;
            }

            return data.Where(profile =>
                profile.Id.ToString().Contains(filterable.Filter) ||
                profile.FirstName.Contains(filterable.Filter) ||
                profile.LastName.Contains(filterable.Filter) ||
                profile.Birthday.ToBirthdayString().Contains(filterable.Filter)
            );
        }

        private IEnumerable<Profile> GetCollection(SortedProfilesModel sortedProfilesModel)
        {
            var profiles = sortedProfilesModel.Profiles;

            profiles = Sort(profiles, sortedProfilesModel);

            profiles = GetPage(profiles, sortedProfilesModel);

            //Thread.Sleep(5000);

            return profiles;
        }

        private IEnumerable<Profile> Sort(IEnumerable<Profile> profiles, ISorterable sorterable)
        {
            switch (sorterable.SortState)
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

            return profiles;
        }

        private IEnumerable<Profile> GetPage(IEnumerable<Profile> profiles, IPageInfo sorterable)
        {
            return profiles
                .Skip((sorterable.PageNumber - 1) * sorterable.PageSize)
                .Take(sorterable.PageSize);
        }
    }
}
