using System.Collections.Generic;

namespace PermissionsAttribute.DAL.Models
{
    public class ProfilePermission
    {
        public List<string> PermissionNames { get; set; }

        public int Id { get; set; }
    }
}
