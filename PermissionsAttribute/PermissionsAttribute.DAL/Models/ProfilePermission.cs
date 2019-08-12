using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionsAttribute.DAL.Models
{
    public class ProfilePermission
    {
        public List<string> PermissionNames { get; set; }

        public int Id { get; set; }
    }
}
