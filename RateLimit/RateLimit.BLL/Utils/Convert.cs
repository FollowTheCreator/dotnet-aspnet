using RateLimit.BLL.Models.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace RateLimit.BLL.Utils
{
    class Convert
    {
        public static TOut To<TIn, TOut>(TIn input) where TOut : class
        {
            if (input.GetType() == typeof(DAL.Models.Profile))
            {
                var data = input as DAL.Models.Profile;
                return new Profile
                {
                    Id = data.Id,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Birthday = data.Birthday
                } as TOut;
            }

            return default;
        }
    }
}
