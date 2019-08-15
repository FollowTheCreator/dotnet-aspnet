using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RequestTimeTrackingMiddleware.WebUI.Utils.TimeTracking
{
    public class TimeTrackingMiddleware
    {
        private readonly RequestDelegate _next;

        public TimeTrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();
            await _next.Invoke(context);
            sw.Stop();
            await context.Response.WriteAsync($"{sw.ElapsedMilliseconds} ms");
        }
    }
}
