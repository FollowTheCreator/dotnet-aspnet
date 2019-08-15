using PermissionsAttribute.BLL.Models.RoleModels;

namespace PermissionsAttribute.BLL.Models.ProfileModels
{
    public class BLLProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public int? RoleId { get; set; }

        public BLLRole Role { get; set; }
    }
}
