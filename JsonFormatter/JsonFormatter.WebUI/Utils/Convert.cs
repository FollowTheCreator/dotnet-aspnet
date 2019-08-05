using JsonFormatter.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonFormatter.WebUI.Utils
{
    public class Convert
    {
        public static TOut To<TIn, TOut>(TIn input) where TOut : class
        {
            if (input.GetType() == typeof(DAL.Models.Profile))
            {
                var data = input as DAL.Models.Profile;
                return new ProfileModel
                {
                    Id = data.Id,
                    Name = data.Name,
                    Email = data.Email
                } as TOut;
            }

            return default;
        }
    }
}
