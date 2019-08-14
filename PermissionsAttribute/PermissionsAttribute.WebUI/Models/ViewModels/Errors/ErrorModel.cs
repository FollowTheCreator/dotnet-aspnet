﻿using System.Collections.Generic;

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
