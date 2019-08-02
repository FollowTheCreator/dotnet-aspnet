using Microsoft.Extensions.Configuration;

namespace Utils
{
    public class Config
    {
        private readonly IConfiguration _config;

        public Config(IConfiguration config)
        {
            _config = config;
        }

        public string GetJsonPath()
        {
            return _config.GetSection("AppSettings")["JsonFilePath"];
        }
    }
}
