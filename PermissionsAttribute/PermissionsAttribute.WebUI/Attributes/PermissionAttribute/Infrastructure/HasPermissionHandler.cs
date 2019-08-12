using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebUI.Attributes.PermissionAttribute.Infrastructure
{
    public class HasPermissionHandler : AuthorizationHandler<HasPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Value == requirement.Role.ToString()))
            {
                return Task.FromResult(0);
            }

            context.Succeed(requirement);

            return Task.FromResult(0);
        }
    }
}
