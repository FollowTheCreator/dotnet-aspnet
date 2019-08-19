using RequestTimeTrackingMiddleware.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RequestTimeTrackingMiddleware.BLL.Utils
{
    static class Convert
    {
        private static readonly Dictionary<TypeKey, Func<object, object>> _converters = new Dictionary<TypeKey, Func<object, object>>
        {
            { new TypeKey { TIn = typeof(Profile), TOut = typeof(DAL.Models.Profile) }, ProfileToDALProfile },
            { new TypeKey { TIn = typeof(DAL.Models.Profile), TOut = typeof(Profile) }, DALProfileToProfile }
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

        private static object ProfileToDALProfile(object input)
        {
            var data = input as Profile;
            return new DAL.Models.Profile
            {
                Id = data.Id,
                Name = data.Name,
                LastName = data.LastName,
            };
        }

        private static object DALProfileToProfile(object input)
        {
            var data = input as DAL.Models.Profile;
            return new Profile
            {
                Id = data.Id,
                Name = data.Name,
                LastName = data.LastName,
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
