using Newtonsoft.Json;
using RateLimit.DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RateLimit.DAL.Contexts
{
    public class JsonContext
    {
        public JsonContext(string path)
        {
            Profiles = JsonConvert.DeserializeObject<IEnumerable<Profile>>(File.ReadAllText(path));
        }

        public IEnumerable<Profile> Profiles { get; set; }
    }
}
