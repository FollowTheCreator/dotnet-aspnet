﻿using PermissionsAttribute.BLL.Models.Authentication;
using PermissionsAttribute.BLL.Models.ProfileModels;
using System.Threading.Tasks;

namespace PermissionsAttribute.BLL.Services.AccountService
{
    public interface IAccountService
    {
        Task<ProfilePermission> RegisterProfileAsync(RegisterModel profile);

        Task<ProfilePermission> GetPermissionsAsync(Profile profile);

        Task<bool> IsEmailExistsAsync(string email);

        Task<ProfilePermission> LogIn(LoginModel model);
    }
}
