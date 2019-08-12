using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebUI.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Name is null")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is null")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is null")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is not confirmed")]
        public string ConfirmPassword { get; set; }
    }
}
