using System;
using System.Collections.Generic;

namespace PermissionsAttribute.BLL.Models
{
    public partial class Role
    {
        public Role()
        {
            Profile = new HashSet<Profile>();
            RolePermission = new HashSet<RolePermission>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Profile> Profile { get; set; }

        public ICollection<RolePermission> RolePermission { get; set; }
    }
}
