using Microsoft.Extensions.Configuration;

namespace PermissionsAttribute.BLL.Services.ConfigService
{
    public class ConfigService : IConfigService
    {
        private const string DefaultRoleSection = "AppSettings:DefaultUserRole";

        private readonly IConfiguration _config;

        public ConfigService(IConfiguration config)
        {
            _config = config;
        }

        public string GetDefaultUserRole()
        {
            return _config.GetSection(DefaultRoleSection).Value;
        }
    }
}
