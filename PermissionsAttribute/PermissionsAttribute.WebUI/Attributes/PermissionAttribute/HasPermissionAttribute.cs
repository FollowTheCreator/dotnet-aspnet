using Microsoft.AspNetCore.Authorization;
using PermissionsAttribute.WebUI.Models;

namespace PermissionsAttribute.WebUI.Attributes.PermissionAttribute
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permissions permission)
        {
            Permission = permission;
        }

        private Permissions _permission;

        public Permissions Permission
        {
            get
            {
                return _permission;
            }
            set
            {
                _permission = value;
                Policy = $"{"HasPermission"}.{value}";
            }
        }
    }
}
