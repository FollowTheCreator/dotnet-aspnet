using PermissionsAttribute.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionsAttribute.BLL.Utils
{
    class Convert
    {
        public static TOut To<TIn, TOut>(TIn input) where TOut : class
        {
            if (input == null)
            {
                return default;
            }

            if (typeof(TIn) == typeof(DAL.Models.Profile))
            {
                var data = input as DAL.Models.Profile;
                if(typeof(TOut) == typeof(BLLProfile))
                {
                    return new BLLProfile
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Email = data.Email,
                        PasswordHash = data.PasswordHash,
                        RoleId = data.RoleId,
                        Role = To<DAL.Models.Role, BLLRole>(data.Role)
                    } as TOut;
                }
                if(typeof(TOut) == typeof(Profile))
                {
                    return new Profile
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Email = data.Email,
                        PasswordHash = data.PasswordHash,
                        RoleId = data.RoleId,
                        Role = To<DAL.Models.Role, Role>(data.Role)
                    } as TOut;
                }
            }

            if (typeof(TIn) == typeof(DAL.Models.ProfilePermission))
            {
                var data = input as DAL.Models.ProfilePermission;
                return new ProfilePermission
                {
                    PermissionNames = data.PermissionNames,
                    Id = data.Id
                } as TOut;
            }

            if (typeof(TIn) == typeof(RegisterModel))
            {
                var data = input as RegisterModel;
                return new DAL.Models.Profile
                {
                    Name = data.Name,
                    Email = data.Email,
                    PasswordHash = data.Password
                } as TOut;
            }

            if (typeof(TIn) == typeof(AddProfileModel))
            {
                var data = input as AddProfileModel;
                return new DAL.Models.Profile
                {
                    Name = data.Name,
                    Email = data.Email,
                    PasswordHash = data.Password
                } as TOut;
            }

            if (typeof(TIn) == typeof(Profile))
            {
                var data = input as Profile;
                return new DAL.Models.Profile
                {
                    Id = data.Id,
                    Name = data.Name,
                    Email = data.Email,
                    PasswordHash = data.PasswordHash,
                    RoleId = data.RoleId,
                    Role = To<Role, DAL.Models.Role>(data.Role)
                } as TOut;
            }

            if (typeof(TIn) == typeof(DAL.Models.Role))
            {
                var data = input as DAL.Models.Role;
                if(typeof(TOut) == typeof(BLLRole))
                {
                    return new BLLRole
                    {
                        Id = data.Id,
                        Name = data.Name
                    } as TOut;
                }
                if(typeof(TOut) == typeof(Role))
                {
                    return new Role
                    {
                        Id = data.Id,
                        Name = data.Name
                    } as TOut;
                }
            }

            if (typeof(TIn) == typeof(Role))
            {
                var data = input as Role;

                var profilesCollection = new List<DAL.Models.Profile>();
                foreach (var profile in data.Profile)
                {
                    profilesCollection.Add(To<Profile, DAL.Models.Profile>(profile));
                }

                var rolePermissionCollection = new List<DAL.Models.RolePermission>();
                foreach (var rolePermission in data.RolePermission)
                {
                    rolePermissionCollection.Add(To<RolePermission, DAL.Models.RolePermission>(rolePermission));
                }

                return new DAL.Models.Role
                {
                    Id = data.Id,
                    Name = data.Name,
                    Profile = profilesCollection,
                    RolePermission = rolePermissionCollection
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

            if (typeof(TIn) == typeof(RolePermission))
            {
                var data = input as RolePermission;
                return new DAL.Models.RolePermission
                {
                    Id = data.Id,
                    Permission = To<Permission, DAL.Models.Permission>(data.Permission),
                    Role = To<Role, DAL.Models.Role>(data.Role),
                    PermissionId = data.PermissionId,
                    RoleId = data.RoleId
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

            if (typeof(TIn) == typeof(Permission))
            {
                var data = input as Permission;

                var rolePermissionCollection = new List<DAL.Models.RolePermission>();
                foreach (var rolePermission in data.RolePermission)
                {
                    rolePermissionCollection.Add(To<RolePermission, DAL.Models.RolePermission>(rolePermission));
                }

                return new DAL.Models.Permission
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
