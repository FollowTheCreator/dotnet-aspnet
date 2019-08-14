using System.ComponentModel.DataAnnotations;

namespace PermissionsAttribute.WebUI.Models.ViewModels.Authentication
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email is null")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is null")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
