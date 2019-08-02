using Newtonsoft.Json;

namespace RateLimit.WebUI.Utils
{
    public class Convert
    {
        public static TOut To<TOut>(object input)
        {
            return JsonConvert.DeserializeObject<TOut>(JsonConvert.SerializeObject(input));
        }
    }
}
