using PermissionsAttribute.BLL.Models.RoleModels;

namespace PermissionsAttribute.BLL.Models.ProfileModels
{
    public partial class Profile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public int? RoleId { get; set; }

        public Role Role { get; set; }
    }
}
