using JsonFormatter.WebUI.Models;

namespace JsonFormatter.WebUI.Utils
{
    public class Convert
    {
        public static TOut To<TIn, TOut>(TIn input) where TOut : class
        {
            if (typeof(TIn) == typeof(DAL.Models.Profile))
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
