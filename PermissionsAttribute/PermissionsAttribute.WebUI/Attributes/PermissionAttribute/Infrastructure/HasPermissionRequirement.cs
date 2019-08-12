using Microsoft.AspNetCore.Authorization;
using PermissionsAttribute.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebUI.Attributes.PermissionAttribute.Infrastructure
{
    public class HasPermissionRequirement : IAuthorizationRequirement
    {
        public HasPermissionRequirement(Permissions role)
        {
            Role = role;
        }

        public Permissions Role { get; private set; }
    }
}
