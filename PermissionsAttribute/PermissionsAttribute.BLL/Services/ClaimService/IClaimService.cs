using System.Collections.Generic;
using System.Security.Claims;

namespace PermissionsAttribute.BLL.Services.ClaimService
{
    public interface IClaimService
    {
        void AddPermissions(IEnumerable<string> permissionNames);

        void AddId(int id);

        void AddEmail(string email);

        bool IsCurrentUser(int id);

        ClaimsIdentity GetClaimsIdentity();

        IEnumerable<string> GetPermissions();
    }
}
