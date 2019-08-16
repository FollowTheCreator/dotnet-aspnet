using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PermissionsAttribute.BLL.Services.ClaimService
{
    public class ClaimService : IClaimService
    {
        private const string IdType = "id";

        private const string EmailType = "email";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            Claims = new List<Claim>();
        }

        public static List<Claim> Claims { get; set; }

        public void AddEmail(string email)
        {
            Claims.Add(new Claim(EmailType, email));
        }

        public void AddId(int id)
        {
            Claims.Add(new Claim(IdType, id.ToString()));
        }

        public void AddPermissions(IEnumerable<string> permissionNames)
        {
            foreach (var permissionName in permissionNames)
            {
                Claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, permissionName));
            }
        }

        public bool IsCurrentUser(int id)
        {
            var currentId = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .FirstOrDefault(c => c.Type == IdType)
                .Value;

            return id.ToString() == currentId;
        }

        public ClaimsIdentity GetClaimsIdentity()
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                Claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );

            return claimsIdentity;
        }

        public IEnumerable<string> GetPermissions()
        {
            return _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .Where(claim => claim.Type == ClaimsIdentity.DefaultRoleClaimType)
                .Select(claim => claim.Value);
        }
    }
}
