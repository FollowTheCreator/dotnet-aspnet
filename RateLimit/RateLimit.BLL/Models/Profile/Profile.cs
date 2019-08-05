using System;
using System.Collections.Generic;
using System.Text;

namespace RateLimit.BLL.Models.Profile
{
    public class Profile
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }
    }
}
