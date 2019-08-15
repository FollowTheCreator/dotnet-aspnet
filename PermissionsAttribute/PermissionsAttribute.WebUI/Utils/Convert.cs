using PermissionsAttribute.WebUI.Models;
using PermissionsAttribute.WebUI.Models.ViewModels;
using PermissionsAttribute.WebUI.Models.ViewModels.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PermissionsAttribute.WebUI.Utils
{
    static class Convert
    {
        private static readonly Dictionary<TypeKey, Func<object, object>> _converters = new Dictionary<TypeKey, Func<object, object>>
        {
            { new TypeKey { TIn = typeof(BLL.Models.ProfileModels.BLLProfile), TOut = typeof(ProfileViewModel) }, BLLBllProfileToProfileViewModel },
            { new TypeKey { TIn = typeof(BLL.Models.ProfileModels.BLLProfile), TOut = typeof(UpdateProfileModel) }, BLLBllProfileToUpdateProfileModel },
            { new TypeKey { TIn = typeof(UpdateProfileModel), TOut = typeof(BLL.Models.ProfileModels.UpdateProfileModel) }, UpdateProfileModelToBLLUpdateProfileModel },
            { new TypeKey { TIn = typeof(BLL.Models.ProfileModels.ProfilePermission), TOut = typeof(ProfilePermission) }, BLLProfilePermissionToProfilePermission },
            { new TypeKey { TIn = typeof(LoginModel), TOut = typeof(BLL.Models.Authentication.LoginModel) }, LoginModelToBLLLoginModel },
            { new TypeKey { TIn = typeof(RegisterModel), TOut = typeof(BLL.Models.Authentication.RegisterModel) }, RegisterModelToBLLRegisterModel },
            { new TypeKey { TIn = typeof(AddProfileModel), TOut = typeof(BLL.Models.ProfileModels.AddProfileModel) }, AddProfileModelToBLLAddProfileModel }
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

        private static object BLLBllProfileToProfileViewModel(object input)
        {
            var data = input as BLL.Models.ProfileModels.BLLProfile;
            return new ProfileViewModel
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                Role = data.Role.Name
            };
        }

        private static object BLLBllProfileToUpdateProfileModel(object input)
        {
            var data = input as BLL.Models.ProfileModels.BLLProfile;
            return new UpdateProfileModel
            {
                Id = data.Id,
                Role = data.Role.Name,
                Name = data.Name,
                Email = data.Email,
                Password = data.PasswordHash,
            };
        }

        private static object UpdateProfileModelToBLLUpdateProfileModel(object input)
        {
            var data = input as UpdateProfileModel;
            return new BLL.Models.ProfileModels.UpdateProfileModel
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                Password = data.Password,
                Role = data.Role
            };
        }

        private static object BLLProfilePermissionToProfilePermission(object input)
        {
            var data = input as BLL.Models.ProfileModels.ProfilePermission;
            return new ProfilePermission
            {
                PermissionNames = data.PermissionNames,
                Id = data.Id
            };
        }

        private static object LoginModelToBLLLoginModel(object input)
        {
            var data = input as LoginModel;
            return new BLL.Models.Authentication.LoginModel
            {
                Password = data.Password,
                Email = data.Email
            };
        }

        private static object RegisterModelToBLLRegisterModel(object input)
        {
            var data = input as RegisterModel;
            return new BLL.Models.Authentication.RegisterModel
            {
                Name = data.Name,
                Email = data.Email,
                Password = data.Password
            };
        }

        private static object AddProfileModelToBLLAddProfileModel(object input)
        {
            var data = input as AddProfileModel;
            return new BLL.Models.ProfileModels.AddProfileModel
            {
                Name = data.Name,
                Email = data.Email,
                Password = data.Password
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
