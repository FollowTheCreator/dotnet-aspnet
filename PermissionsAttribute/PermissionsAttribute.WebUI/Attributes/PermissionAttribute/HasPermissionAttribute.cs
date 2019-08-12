using Microsoft.AspNetCore.Authorization;
using PermissionsAttribute.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebUI.Attributes.PermissionAttribute
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(Permissions role)
        {
            Role = role;
        }

        private Permissions _role;

        public Permissions Role
        {
            get
            {
                return _role;
            }
            set
            {
                _role = value;
                Policy = $"{"HasPermission"}.{value}";
            }
        }
    }
}
