using Newtonsoft.Json;
using RateLimit.DAL.Models;
using System.Collections.Generic;
using System.IO;

namespace RateLimit.DAL.Contexts
{
    public class JsonContext : IContext
    {
        public JsonContext(string path)
        {
            Profiles = JsonConvert.DeserializeObject<IEnumerable<Profile>>(File.ReadAllText(path));
        }

        public IEnumerable<Profile> Profiles { get; set; }
    }
}
