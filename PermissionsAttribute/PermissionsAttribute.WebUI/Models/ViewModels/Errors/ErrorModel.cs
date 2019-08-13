using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PermissionsAttribute.WebUI.Models.ViewModels.Errors
{
    public class ErrorModel
    {
        public ErrorModel()
        {
            Permissions = new List<string>();
        }

        public List<string> Permissions { get; set; }
    }
}
