using PermissionsAttribute.WebUI.Models;
using PermissionsAttribute.WebUI.Models.ViewModels;
using PermissionsAttribute.WebUI.Models.ViewModels.Authentication;
using System.Collections.Generic;

namespace PermissionsAttribute.WebUI.Utils
{
    class Convert
    {
        public static TOut To<TIn, TOut>(TIn input) where TOut : class
        {
            if(input == null)
            {
                return default;
            }

            if (typeof(TIn) == typeof(BLL.Models.BLLProfile))
            {
                var data = input as BLL.Models.BLLProfile;
                if(typeof(TOut) == typeof(ProfileViewModel))
                {
                    return new ProfileViewModel
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Email = data.Email,
                        Role = data.Role.Name
                    } as TOut;
                }
                if (typeof(TOut) == typeof(UpdateProfileModel))
                {
                    return new UpdateProfileModel
                    {
                        Id = data.Id,
                        Role = data.Role.Name,
                        Name = data.Name,
                        Email = data.Email,
                        Password = data.PasswordHash,
                    } as TOut;
                }
            }

            if (typeof(TIn) == typeof(UpdateProfileModel))
            {
                var data = input as UpdateProfileModel;
                return new BLL.Models.Profile
                {
                    Id = data.Id,
                    Name = data.Name,
                    Email = data.Email,
                    PasswordHash = data.Password,
                    Role = new BLL.Models.Role { Name = data.Role }
                } as TOut;
            }

            if (typeof(TIn) == typeof(BLL.Models.ProfilePermission))
            {
                var data = input as BLL.Models.ProfilePermission;
                return new ProfilePermission
                {
                    PermissionNames = data.PermissionNames,
                    Id = data.Id
                } as TOut;
            }

            if (typeof(TIn) == typeof(LoginModel))
            {
                var data = input as LoginModel;
                return new BLL.Models.Profile
                {
                    PasswordHash = data.Password,
                    Email = data.Email
                } as TOut;
            }

            if (typeof(TIn) == typeof(RegisterModel))
            {
                var data = input as RegisterModel;
                return new BLL.Models.RegisterModel
                {
                    Name = data.Name,
                    Email = data.Email,
                    Password = data.Password
                } as TOut;
            }

            if (typeof(TIn) == typeof(AddProfileModel))
            {
                var data = input as AddProfileModel;
                return new BLL.Models.AddProfileModel
                {
                    Name = data.Name,
                    Email = data.Email,
                    Password = data.Password
                } as TOut;
            }

            if (typeof(TIn) == typeof(DAL.Models.Profile))
            {
                var data = input as DAL.Models.Profile;
                return new ProfileViewModel
                {
                    Id = data.Id,
                    Name = data.Name,
                    Email = data.Email,
                    Role = data.Role.Name
                } as TOut;
            }

            if (typeof(TIn) == typeof(BLL.Models.Role))
            {
                var data = input as BLL.Models.Role;

                var profilesCollection = new List<Profile>();
                foreach(var profile in data.Profile)
                {
                    profilesCollection.Add(To<BLL.Models.Profile, Profile>(profile));
                }

                var rolePermissionCollection = new List<RolePermission>();
                foreach (var rolePermission in data.RolePermission)
                {
                    rolePermissionCollection.Add(To<BLL.Models.RolePermission, RolePermission>(rolePermission));
                }

                return new Role
                {
                    Id = data.Id,
                    Name = data.Name,
                    Profile = profilesCollection,
                    RolePermission = rolePermissionCollection
                } as TOut;
            }

            if (typeof(TIn) == typeof(DAL.Models.Role))
            {
                var data = input as DAL.Models.Role;

                var profilesCollection = new List<Profile>();
                foreach (var profile in data.Profile)
                {
                    profilesCollection.Add(To<DAL.Models.Profile, Profile>(profile));
                }

                var rolePermissionCollection = new List<RolePermission>();
                foreach (var rolePermission in data.RolePermission)
                {
                    rolePermissionCollection.Add(To<DAL.Models.RolePermission, RolePermission>(rolePermission));
                }

                return new Role
                {
                    Id = data.Id,
                    Name = data.Name,
                    Profile = profilesCollection,
                    RolePermission = rolePermissionCollection
                } as TOut;
            }

            if (typeof(TIn) == typeof(BLL.Models.RolePermission))
            {
                var data = input as BLL.Models.RolePermission;
                return new RolePermission
                {
                    Id = data.Id,
                    Permission = To<BLL.Models.Permission, Permission>(data.Permission),
                    Role = To<BLL.Models.Role, Role>(data.Role),
                    PermissionId = data.PermissionId,
                    RoleId = data.RoleId
                } as TOut;
            }

            if (typeof(TIn) == typeof(DAL.Models.RolePermission))
            {
                var data = input as DAL.Models.RolePermission;
                return new RolePermission
                {
                    Id = data.Id,
                    Permission = To<DAL.Models.Permission, Permission>(data.Permission),
                    Role = To<DAL.Models.Role, Role>(data.Role),
                    PermissionId = data.PermissionId,
                    RoleId = data.RoleId
                } as TOut;
            }

            if (typeof(TIn) == typeof(BLL.Models.Permission))
            {
                var data = input as BLL.Models.Permission;

                var rolePermissionCollection = new List<RolePermission>();
                foreach (var rolePermission in data.RolePermission)
                {
                    rolePermissionCollection.Add(To<BLL.Models.RolePermission, RolePermission>(rolePermission));
                }

                return new Permission
                {
                    Id = data.Id,
                    Name = data.Name,
                    RolePermission = rolePermissionCollection
                } as TOut;
            }

            if (typeof(TIn) == typeof(DAL.Models.Permission))
            {
                var data = input as DAL.Models.Permission;

                var rolePermissionCollection = new List<RolePermission>();
                foreach (var rolePermission in data.RolePermission)
                {
                    rolePermissionCollection.Add(To<DAL.Models.RolePermission, RolePermission>(rolePermission));
                }

                return new Permission
                {
                    Id = data.Id,
                    Name = data.Name,
                    RolePermission = rolePermissionCollection
                } as TOut;
            }

            return default;
        }
    }
}
