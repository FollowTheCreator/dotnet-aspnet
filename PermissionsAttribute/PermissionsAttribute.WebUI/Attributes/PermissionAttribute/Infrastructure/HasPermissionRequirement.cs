using Microsoft.AspNetCore.Authorization;
using PermissionsAttribute.WebUI.Models;

namespace PermissionsAttribute.WebUI.Attributes.PermissionAttribute.Infrastructure
{
    public class HasPermissionRequirement : IAuthorizationRequirement
    {
        public HasPermissionRequirement(Permissions permission)
        {
            Permission = permission;
        }

        public Permissions Permission { get; private set; }
    }
}
