﻿using RateLimit.BLL.Models.Profile;

namespace RateLimit.BLL.Services.ProfileService
{
    public interface IProfileService
    {
        SearchResult Search(ProfilesSearchRequest actionModel);
    }
}
