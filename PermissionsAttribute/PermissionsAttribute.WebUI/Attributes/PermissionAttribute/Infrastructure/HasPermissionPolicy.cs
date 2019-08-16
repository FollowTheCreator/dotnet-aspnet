using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using PermissionsAttribute.WebUI.Models;
using System;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebUI.Attributes.PermissionAttribute.Infrastructure
{
    public class HasPermissionPolicy : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider DefaultPolicyProvider { get; }

        public HasPermissionPolicy(IOptions<AuthorizationOptions> options)
        {
            DefaultPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return DefaultPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            string[] subStringPolicy = policyName.Split('.');
            if (subStringPolicy.Length > 1 && subStringPolicy[0].Equals("HasPermission", StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();

                policy.AddRequirements(new HasPermissionRequirement(GetPermission(subStringPolicy[1])));
                return Task.FromResult(policy.Build());
            }
            return DefaultPolicyProvider.GetPolicyAsync(policyName);
        }

        private static Permissions GetPermission(string stringPermission)
        {
            if(Enum.TryParse(stringPermission, out Permissions permissions)){
                return permissions;
            }

            return default;
        }
    }
}
