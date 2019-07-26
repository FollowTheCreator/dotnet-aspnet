using System;
using System.Text;

namespace ITechart.DotNet.AspNet.CustomModelBinder.Utils
{
    public static class Base64Coder
    {
        public static Guid DecodeToGuid(this string base64String)
        {
            var base64Decoded = Convert.FromBase64String(base64String);
            return Guid.Parse(Encoding.UTF8.GetString(base64Decoded));
        }
    }
}
