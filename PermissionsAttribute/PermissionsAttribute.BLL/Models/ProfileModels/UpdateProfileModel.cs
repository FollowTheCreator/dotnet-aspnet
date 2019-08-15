using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionsAttribute.BLL.Models.ProfileModels
{
    public class UpdateProfileModel
    {
        public int Id { get; set; }

        public string Role { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
