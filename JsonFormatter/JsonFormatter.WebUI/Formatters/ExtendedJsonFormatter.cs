using JsonFormatter.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonFormatter.WebUI.Formatters
{
    public class ExtendedJsonFormatter : TextOutputFormatter
    {
        public string ContentType { get; }
        public ExtendedJsonFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json+custom"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (
                typeof(ProfileModel).IsAssignableFrom(type) ||
                typeof(IEnumerable<ProfileModel>).IsAssignableFrom(type)
            )
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;

            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<ProfileModel>)
            {
                foreach (var profile in context.Object as IEnumerable<ProfileModel>)
                {
                    FormatExtendedJson(buffer, profile);
                }
            }
            else
            {
                var profile = context.Object as ProfileModel;
                FormatExtendedJson(buffer, profile);
            }
            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatExtendedJson(StringBuilder buffer, ProfileModel person)
        {
            buffer.Append(JsonConvert.SerializeObject(person));
        }
    }
}
