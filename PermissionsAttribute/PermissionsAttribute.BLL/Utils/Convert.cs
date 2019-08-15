using PermissionsAttribute.BLL.Models;
using PermissionsAttribute.BLL.Models.Authentication;
using PermissionsAttribute.BLL.Models.ProfileModels;
using PermissionsAttribute.BLL.Models.RoleModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PermissionsAttribute.BLL.Utils
{
    static class Convert
    {
        private static readonly Dictionary<TypeKey, Func<object, object>> _converters = new Dictionary<TypeKey, Func<object, object>>
        {
            { new TypeKey { TIn = typeof(DAL.Models.Profile), TOut = typeof(BLLProfile) }, DALProfileToBllProfile },
            { new TypeKey { TIn = typeof(LoginModel), TOut = typeof(Profile) }, LoginModelToProfile },
            { new TypeKey { TIn = typeof(UpdateProfileModel), TOut = typeof(Profile) }, UpdateProfileToProfile },
            { new TypeKey { TIn = typeof(DAL.Models.Profile), TOut = typeof(Profile) }, DALProfileToProfile },
            { new TypeKey { TIn = typeof(DAL.Models.ProfilePermission), TOut = typeof(ProfilePermission) }, DALProfilePermissionToProfilePermission },
            { new TypeKey { TIn = typeof(RegisterModel), TOut = typeof(DAL.Models.Profile) }, RegisterModelToDALProfile },
            { new TypeKey { TIn = typeof(AddProfileModel), TOut = typeof(DAL.Models.Profile) }, AddProfileModelToDALProfile },
            { new TypeKey { TIn = typeof(Profile), TOut = typeof(DAL.Models.Profile) }, ProfileToDALProfile },
            { new TypeKey { TIn = typeof(DAL.Models.Role), TOut = typeof(BLLRole) }, DALRoleToBllRole },
            { new TypeKey { TIn = typeof(DAL.Models.Role), TOut = typeof(Role) }, DALRoleToRole },
            { new TypeKey { TIn = typeof(Role), TOut = typeof(DAL.Models.Role) }, RoleToDALRole },
            { new TypeKey { TIn = typeof(RolePermission), TOut = typeof(DAL.Models.RolePermission) }, RolePermissionToDALRolePermission },
            { new TypeKey { TIn = typeof(Permission), TOut = typeof(DAL.Models.Permission) }, PermissionToDALPermission }
        };

        private class TypeKey
        {
            public Type TIn { get; set; }

            public Type TOut { get; set; }

            public override bool Equals(object obj)
            {
                if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                {
                    return false;
                }
                else
                {
                    TypeKey tk = (TypeKey)obj;
                    return (TIn == tk.TIn) && (tk.TOut == TOut);
                }
            }

            public override int GetHashCode()
            {
                return (TIn.GetHashCode() << 2) ^ TOut.GetHashCode();
            }
        }

        private static object DALProfileToBllProfile(object input)
        {
            var data = input as DAL.Models.Profile;

            return new BLLProfile
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                PasswordHash = data.PasswordHash,
                RoleId = data.RoleId,
                Role = To<DAL.Models.Role, BLLRole>(data.Role)
            };
        }

        private static object LoginModelToProfile(object input)
        {
            var data = input as LoginModel;
            return new Profile
            {
                PasswordHash = data.Password,
                Email = data.Email
            };
        }

        private static object UpdateProfileToProfile(object input)
        {
            var data = input as UpdateProfileModel;

            return new Profile
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                PasswordHash = data.Password,
                Role = new Role
                {
                    Name = data.Role
                }
            };
        }

        private static object DALProfileToProfile(object input)
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
            };
        }

        private static object DALProfilePermissionToProfilePermission(object input)
        {
            var data = input as DAL.Models.ProfilePermission;

            return new ProfilePermission
            {
                PermissionNames = data.PermissionNames,
                Id = data.Id
            };
        }

        private static object RegisterModelToDALProfile(object input)
        {
            var data = input as RegisterModel;

            return new DAL.Models.Profile
            {
                Name = data.Name,
                Email = data.Email,
                PasswordHash = data.Password
            };
        }

        private static object AddProfileModelToDALProfile(object input)
        {
            var data = input as AddProfileModel;

            return new DAL.Models.Profile
            {
                Name = data.Name,
                Email = data.Email,
                PasswordHash = data.Password
            };
        }

        private static object ProfileToDALProfile(object input)
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
            };
        }
        private static object DALRoleToBllRole(object input)
        {
            var data = input as DAL.Models.Role;

            return new BLLRole
            {
                Id = data.Id,
                Name = data.Name
            };
        }

        private static object DALRoleToRole(object input)
        {
            var data = input as DAL.Models.Role;

            return new Role
            {
                Id = data.Id,
                Name = data.Name
            };
        }

        private static object RoleToDALRole(object input)
        {
            var data = input as Role;

            var profilesCollection =
                (ICollection<DAL.Models.Profile>)To<Profile, DAL.Models.Profile>(data.Profile).ToList();

            var rolePermissionCollection =
                (ICollection<DAL.Models.RolePermission>)To<RolePermission, DAL.Models.RolePermission>(data.RolePermission).ToList();

            return new DAL.Models.Role
            {
                Id = data.Id,
                Name = data.Name,
                Profile = profilesCollection,
                RolePermission = rolePermissionCollection
            };
        }

        private static object RolePermissionToDALRolePermission(object input)
        {
            var data = input as RolePermission;

            return new DAL.Models.RolePermission
            {
                Id = data.Id,
                Permission = To<Permission, DAL.Models.Permission>(data.Permission),
                Role = To<Role, DAL.Models.Role>(data.Role),
                PermissionId = data.PermissionId,
                RoleId = data.RoleId
            };
        }

        private static object PermissionToDALPermission(object input)
        {
            var data = input as Permission;

            var rolePermissionCollection =
                (ICollection<DAL.Models.RolePermission>)To<RolePermission, DAL.Models.RolePermission>(data.RolePermission);

            return new DAL.Models.Permission
            {
                Id = data.Id,
                Name = data.Name,
                RolePermission = rolePermissionCollection
            };
        }

        public static IEnumerable<TOut> To<TIn, TOut>(IEnumerable<TIn> input) where TOut : class
        {
            return input.Select(i => To<TIn, TOut>(i));
        }

        public static TOut To<TIn, TOut>(TIn input) where TOut : class
        {
            if (input == null)
            {
                return default;
            }

            if (_converters.TryGetValue(new TypeKey { TIn = typeof(TIn), TOut = typeof(TOut) }, out Func<object, object> func))
            {
                return (TOut)func?.Invoke(input);
            }

            return default;
        }
    }
}
