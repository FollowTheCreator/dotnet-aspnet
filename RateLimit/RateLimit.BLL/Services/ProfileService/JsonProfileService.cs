using RateLimit.BLL.Models;
using RateLimit.BLL.Models.Profile;
using RateLimit.DAL.Repositoriy;
using System;
using System.Collections.Generic;

namespace RateLimit.BLL.Services.ProfileService
{
    public class JsonProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public JsonProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public SearchResult Search(ProfilesSearchRequest actionModel)
        {
            BoundariesCheck(actionModel);

            var filteredCollectionCount = _profileRepository.Count(
                new DAL.Models.JsonProfileReposotiry.ProfileFilter { Filter = actionModel.Filter }
            );
            var totalPages = (int)Math.Ceiling(filteredCollectionCount / (double)actionModel.PageSize);
            var profiles = _profileRepository.Search(new DAL.Models.JsonProfileReposotiry.ProfileSearchFilter
            {
                PageNumber = actionModel.PageNumber,
                PageSize = actionModel.PageSize,
                SortState = (DAL.Models.JsonProfileReposotiry.ProfilesSort)actionModel.SortState
            });

            var convertedProfiles = new List<Profile>();
            foreach(DAL.Models.Profile profile in profiles)
            {
                convertedProfiles.Add(Utils.Convert.To<DAL.Models.Profile, Profile>(profile));
            }

            return new SearchResult
            {
                Profiles = convertedProfiles,
                PageInfo = new CollectionInfo
                {
                    PageNumber = actionModel.PageNumber,
                    PageSize = actionModel.PageSize,
                    TotalItems = filteredCollectionCount,
                    TotalPages = totalPages
                }
            };
        }

        private void BoundariesCheck(ProfilesSearchRequest actionModel)
        {
            if (actionModel.PageNumber == 0)
            {
                actionModel.PageNumber = 1;
            }

            if (actionModel.PageSize == 0)
            {
                actionModel.PageSize = 4;
            }
        }
    }
}
