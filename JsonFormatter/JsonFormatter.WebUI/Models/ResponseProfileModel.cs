using Newtonsoft.Json;

namespace JsonFormatter.WebUI.Models
{
    public class ResponseProfileModel
    {
        [JsonProperty("data")]
        public ProfileModel Data { get; set; }

        [JsonProperty("_link")]
        public string Link { get; set; }
    }
}
