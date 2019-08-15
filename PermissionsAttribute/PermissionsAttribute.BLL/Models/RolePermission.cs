﻿using PermissionsAttribute.BLL.Models.RoleModels;

namespace PermissionsAttribute.BLL.Models
{
    public partial class RolePermission
    {
        public int Id { get; set; }

        public int? RoleId { get; set; }

        public int? PermissionId { get; set; }

        public Permission Permission { get; set; }

        public Role Role { get; set; }
    }
}
