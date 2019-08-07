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
