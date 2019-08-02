using Microsoft.Extensions.Configuration;

namespace RateLimit.BLL.Services.Configuration
{
    public class Config : IConfig
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
